using Microsoft.AspNetCore.SignalR;

namespace CallOfSokoHub
{
    public class MainHub : Hub
    {
        MainGameProcess mainGameProcess = MainGameProcess.Instance;

        public void RequestMap()
        {
            mainGameProcess.Clients = Clients;
            mainGameProcess.SendMap();
        }
    }
}
