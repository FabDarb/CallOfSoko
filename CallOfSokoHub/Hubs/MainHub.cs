using CallOfLibrary;
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
        public void PlayerMove(DataPlayer player)
        {
            mainGameProcess.PlayerMove(player);
        }
        public void Shoot(int userId)
        {

        }
    }
}
