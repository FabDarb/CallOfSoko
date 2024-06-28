using CallOfLibrary;
using CallOfSokoClient.Class.BackEnd;
using Microsoft.AspNetCore.SignalR.Client;
using System.Configuration;
using System.Diagnostics;

namespace CallOfSokoClient
{
    public partial class Form1 : Form
    {
        public User MyUser { get; set; }

        Map map = Map.Instance;

        HubConnection? connection;

        Thread displayThread;


        public Form1()
        {
            InitializeComponent();
            MyUser = new User();
            ConnectionToHub();
            displayThread = new Thread(new ThreadStart(UIUpdater));
            displayThread.IsBackground = true;
            displayThread.Start();
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
                connection?.On<List<DataPlayer>>("UpdatePossitionPlayer", (dataPlayer) =>
                {
                    map.UpdateMap(dataPlayer, MyUser);
                    if (!map.IsInit) map.IsInit = true;
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
            foreach (Block block in map.MapDisplay)
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
                if (map.IsInit && MyUser.IsMoving > 0)
                {
                    map.PlayerMove(MyUser);
                    DataPlayer dp = new DataPlayer(map.ActualPlayer!.Id, map.ActualPlayer.X, map.ActualPlayer.Y);
                    connection?.InvokeAsync("PlayerMove", dp);
                }

            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            MyUser.MovementInput[e.KeyCode] = true;
            ++MyUser.IsMoving;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            MyUser.MovementInput[e.KeyCode] = false;
            --MyUser.IsMoving;
        }
    }
}
