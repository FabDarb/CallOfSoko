using CallOfLibrary;
using CallOfSokoClient.Class.BackEnd;
using Microsoft.AspNetCore.SignalR.Client;
using System.Configuration;
using System.Diagnostics;

namespace CallOfSokoClient
{
    public partial class Form1 : Form
    {
        public int UserId { get; set; }
        public Player? player { get; set; }
        HubConnection? connection;
        List<Block> Map = new List<Block>();
        Thread displayThread;
        public Form1()
        {
            InitializeComponent();
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
                connection?.On<List<DataBlock>>("UpdateGame", (datablocks) =>
                {
                    Map.Clear();
                    UpdateMap(datablocks);
                });
                connection?.On<int>("JoiningConfirmed", (id) =>
                {
                    UserId = id;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }


        }
        private void mainDisplay_Paint(object sender, PaintEventArgs e)
        {
            foreach (Block block in Map)
            {
                block.DrawBlock(e, mainDisplay);
            }
        }
        private void UpdateMap(List<DataBlock> datablocks)
        {
            foreach (DataBlock datablock in datablocks)
            {
                switch (datablock.Type)
                {
                    case DataBlockType.Wall:
                        Map.Add(new Wall(datablock.X, datablock.Y));
                        break;
                    case DataBlockType.Player:
                        Map.Add(new Player(datablock.X, datablock.Y));
                        break;
                }
            }
        }
        private void UIUpdater()
        {
            while (true)
            {
                mainDisplay.Invalidate();
                Thread.Sleep(20);
            }
        }
    }
}
