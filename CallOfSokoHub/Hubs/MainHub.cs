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
        public void Shoot(int userId, Dictionary<string, int> shot)
        {
            mainGameProcess.PlayerShoot(userId, shot);
        }

        public void ThisPlayerIsDead(int playerId)
        {
            //todo in EndGame
            Clients.All.SendAsync("ThisPlayerLoose", playerId);
        }
    }
}
