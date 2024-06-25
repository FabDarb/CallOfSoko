using Microsoft.AspNetCore.SignalR;

namespace CallOfSokoHub
{
    public class MainHub : Hub
    {
        MainGameProcess mainGameProcess = MainGameProcess.Instance;

        public void JoinGame()
        {
            mainGameProcess.JoinGame(Clients.Caller);
        }

        public void RequestMap()
        {
            mainGameProcess.Clients = Clients;
            mainGameProcess.SendMap();
        }
    }
}
