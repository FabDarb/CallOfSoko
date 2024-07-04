using CallOfLibrary;
using CallOfSokoClient.Class.BackEnd;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Immutable;
using System.Configuration;
using System.Diagnostics;

namespace CallOfSokoClient
{
    public partial class Form1 : Form
    {
        public User MyUser { get; set; }
        public const int MaxPlayerVelocity = 5;

        Map map = Map.Instance;

        HubConnection? connection;

        Thread displayThread;

        System.Timers.Timer timer;


        public Form1()
        {
            InitializeComponent();
            MyUser = new User();
            ConnectionToHub();
            displayThread = new Thread(new ThreadStart(UIUpdater));
            displayThread.IsBackground = true;
            displayThread.Start();
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            foreach (Bullet bullet in map.BulletList.ToImmutableList())
            {
                if (bullet.LifeTime > 0)
                {
                    --bullet.LifeTime;
                }
                else
                {
                    map.BulletList.Remove(bullet);
                    map.MapDisplay.Remove(bullet);
                }
            }
        }

        private async void ConnectionToHub()
        {
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string HubIP = configuration.AppSettings.Settings["HubIP"].Value;
                connection = new HubConnectionBuilder().WithUrl($"http://{HubIP}:5034/CallOfHub").Build();
                await connection.StartAsync();
                ListOfConnection();
                connection?.InvokeAsync("JoinGame");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void ListOfConnection()
        {
            try
            {
                connection?.On<List<DataBlock>>("CreateMap", (datablocks) =>
                {
                    map.CreateMap(datablocks);

                });
                connection?.On<Updater>("Update", (updater) =>
                {
                    map.UpdateMap(updater.Players, MyUser);
                    if (updater.Bullets != null && updater.Bullets.Count > 0)
                    {
                        map.UpdateShoot(updater.Bullets);
                    }
                    if (!map.IsInit)
                    {
                        map.IsInit = true;
                    }
                });
                connection?.On<int>("JoiningConfirmed", (id) =>
                {
                    MyUser.UserId = id;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }


        }
        private void mainDisplay_Paint(object sender, PaintEventArgs e)
        {
            foreach (Block block in map.MapDisplay.ToImmutableList())
            {
                block.DrawBlock(e, mainDisplay);
            }
        }
        private void UIUpdater()
        {
            while (true)
            {

                mainDisplay.Invalidate();
                Thread.Sleep(20);
                if (map.IsInit)
                {
                    foreach (var movementinput in MyUser.MovementInput.Values)
                    {
                        if (movementinput.IsActive)
                        {
                            if (movementinput.Velocity < MaxPlayerVelocity)
                            {
                                ++movementinput.Velocity;
                            }
                        }
                        else
                        {
                            if (movementinput.Velocity > 0)
                            {
                                --movementinput.Velocity;
                            }
                        }
                    }
                    MovementCollision();
                    map.PlayerMove(MyUser);
                    DataPlayer dp = new DataPlayer(map.ActualPlayer!.Id, map.ActualPlayer.X, map.ActualPlayer.Y, map.ActualPlayer.Angle);
                    connection?.InvokeAsync("PlayerMove", dp);
                }

            }
        }


        private void MovementCollision()
        {
            foreach (Block block in map.MapDisplay.ToImmutableList())
            {
                if (map.ActualPlayer != block && TestCollision(map.ActualPlayer!.HitBox, block.HitBox))
                {
                    var intersection = Rectangle.Intersect(map.ActualPlayer!.HitBox, block.HitBox);
                    bool top = intersection.Top == map.ActualPlayer!.HitBox.Top;
                    bool bottom = intersection.Bottom == map.ActualPlayer!.HitBox.Bottom;
                    bool left = intersection.Left == map.ActualPlayer!.HitBox.Left;
                    bool right = intersection.Right == map.ActualPlayer!.HitBox.Right;

                    if (top)
                    {
                        MyUser.MovementInput[Keys.W].Velocity = 0;
                    }
                    if (bottom)
                    {
                        MyUser.MovementInput[Keys.S].Velocity = 0;
                    }
                    if (left)
                    {
                        MyUser.MovementInput[Keys.A].Velocity = 0;
                    }
                    if (right)
                    {
                        MyUser.MovementInput[Keys.D].Velocity = 0;
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (MyUser.MovementInput.ContainsKey(e.KeyCode))
            {
                MyUser.MovementInput[e.KeyCode].IsActive = true;
                ++MyUser.IsMoving;
            }
            if (e.KeyCode == Keys.R) { map.ActualPlayer!.Gun.Reload(); }
        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (MyUser.MovementInput.ContainsKey(e.KeyCode))
            {
                MyUser.MovementInput[e.KeyCode].IsActive = false;
                --MyUser.IsMoving;
            }
        }

        private bool TestCollision(Rectangle rec1, Rectangle rec2)
        {
            bool test = false;
            if (rec1.IntersectsWith(rec2)) { test = true; }
            return test;
        }

        private void mainDisplay_Click(object sender, EventArgs e)
        {
            Dictionary<string, int>? shot = map.ActualPlayer?.Gun.Shoot();
            if (shot != null)
            {
                connection?.InvokeAsync("Shoot", MyUser.UserId, shot);
            }
        }

        private void mainDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (map.IsInit)
            {
                int angle = (int)Math.Round(Math.Atan2((e.Y - map.ActualPlayer!.Y), e.X - map.ActualPlayer!.X) * 180 / Math.PI);
                map.ActualPlayer!.Angle = angle;
            }
        }
    }
}
