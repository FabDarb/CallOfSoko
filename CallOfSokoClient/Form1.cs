using CallOfLibrary;
using CallOfSokoClient.Class.BackEnd;
using CallOfSokoClient.Class.UI;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Immutable;
using System.Configuration;
using System.Diagnostics;

namespace CallOfSokoClient
{
    public partial class Form1 : Form
    {
        public User MyUser { get; set; }
        public const int MaxPlayerVelocity = 2;

        Map map = Map.Instance;

        HubConnection? connection;

        Thread displayThread;

        System.Timers.Timer timer;
        LifeBar? HealthViewer { get; set; }


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
                    map.RemoveBullet(bullet);
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
                        HealthViewer = new LifeBar(map.ActualPlayer!.Health);
                        map.UpdateLife += Map_UpdateLife;
                        LifePictureBox.Invalidate();
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

        private void Map_UpdateLife(object? sender, EventArgs e)
        {
            HealthViewer!.PlayerLife = map.ActualPlayer!.Health;
            LifePictureBox.Invalidate();
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
                    foreach (var movementinput in MyUser.MovementInput)
                    {
                        if (movementinput.Value)
                        {
                            switch (movementinput.Key)
                            {
                                case Keys.W: if (Math.Abs(MyUser.VerticalVelocity) < MaxPlayerVelocity) --MyUser.VerticalVelocity; break;
                                case Keys.S: if (Math.Abs(MyUser.VerticalVelocity) < MaxPlayerVelocity) ++MyUser.VerticalVelocity; break;
                                case Keys.A: if (Math.Abs(MyUser.HorizontalVelocity) < MaxPlayerVelocity) --MyUser.HorizontalVelocity; break;
                                case Keys.D: if (Math.Abs(MyUser.HorizontalVelocity) < MaxPlayerVelocity) ++MyUser.HorizontalVelocity; break;
                            }
                        }
                        else
                        {
                            switch (movementinput.Key)
                            {
                                case Keys.W: if (MyUser.VerticalVelocity < 0) ++MyUser.VerticalVelocity; break;
                                case Keys.S: if (MyUser.VerticalVelocity > 0) --MyUser.VerticalVelocity; break;
                                case Keys.A: if (MyUser.HorizontalVelocity < 0) ++MyUser.HorizontalVelocity; break;
                                case Keys.D: if (MyUser.HorizontalVelocity > 0) --MyUser.HorizontalVelocity; break;
                            }
                        }
                        MovementCollision();
                        map.PlayerMove(MyUser);
                        DataPlayer dp = new DataPlayer(map.ActualPlayer!.Id, map.ActualPlayer.X, map.ActualPlayer.Y, map.ActualPlayer.Angle);
                        connection?.InvokeAsync("PlayerMove", dp);
                    }

                }
            }
        }

        private void MovementCollision()
        {
            foreach (Block block in map.MapDisplay.ToImmutableList())
            {
                int x = MyUser.HorizontalVelocity;
                int y = MyUser.VerticalVelocity;
                if (map.ActualPlayer != block && TestCollision(map.ActualPlayer!.GetHitBox(), block.HitBox))
                {
                    if (block.GetType() == typeof(Bullet))
                    {
                        Bullet b = (Bullet)block;
                        if (b.IdPlayer != map.ActualPlayer.Id)
                        {
                            map.ActualPlayer!.Health -= b.Damage;
                        }
                        //map.RemoveBullet(b);
                    }
                    else
                    {
                        var intersection = Rectangle.Intersect(map.ActualPlayer!.HitBox, block.HitBox);
                        bool top = intersection.Top == map.ActualPlayer!.HitBox.Top;
                        bool bottom = intersection.Bottom == map.ActualPlayer!.HitBox.Bottom;
                        bool left = intersection.Left == map.ActualPlayer!.HitBox.Left;
                        bool right = intersection.Right == map.ActualPlayer!.HitBox.Right;

                        if (top || bottom)
                        {
                            map.ActualPlayer.Y -= MyUser.VerticalVelocity;
                            MyUser.VerticalVelocity = 0;
                        }
                        if (left || right)
                        {
                            map.ActualPlayer.X -= MyUser.HorizontalVelocity;
                            MyUser.HorizontalVelocity = 0;
                        }
                    }
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (MyUser.MovementInput.ContainsKey(e.KeyCode))
            {
                MyUser.MovementInput[e.KeyCode] = true;
                ++MyUser.IsMoving;
            }
            if (e.KeyCode == Keys.R) { map.ActualPlayer!.Gun.Reload(); }
        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (MyUser.MovementInput.ContainsKey(e.KeyCode))
            {
                MyUser.MovementInput[e.KeyCode] = false;
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

        private void LifePictureBox_Paint(object sender, PaintEventArgs e)
        {
            Debug.WriteLine("Called");
            if (map.IsInit)
            {
                HealthViewer!.Update(e);
            }
        }
    }
}
