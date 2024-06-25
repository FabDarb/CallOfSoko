using Microsoft.AspNetCore.SignalR;

namespace CallOfSokoHub
{
    public class MainGameProcess
    {
        static public MainGameProcess Instance { get; } = new MainGameProcess();
        public Model model { get; set; }
        public IHubCallerClients? Clients { get; set; }
        private MainGameProcess()
        {
            model = new Model();
        }

        public void SendMap()
        {
            model.UseTemplateMap();
            Clients?.All.SendAsync("ReceiveMap", model.map);
        }
    }
}
