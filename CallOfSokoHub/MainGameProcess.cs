using Microsoft.AspNetCore.SignalR;

namespace CallOfSokoHub
{
    public class MainGameProcess
    {
        static public MainGameProcess Instance { get; } = new MainGameProcess();
        public Model model { get; set; }
        public IHubCallerClients? Clients { get; set; }
        Directory<> users { get; set; }

        private MainGameProcess()
        {
            model = new Model();
        }

        public void JoinGame()
        {
            
        }

        public void SendMap()
        {
            model.UseTemplateMap();
            Clients?.All.SendAsync("UpdateGame", model.map);
        }
    }
}
