using CallOfLibrary;
using Microsoft.AspNetCore.SignalR;

namespace CallOfSokoHub
{
    public class MainGameProcess
    {
        static public MainGameProcess Instance { get; } = new MainGameProcess();
        public Model model { get; set; }
        public IHubCallerClients? Clients { get; set; }
        public Dictionary<int, User> Users { get; set; }

        private MainGameProcess()
        {
            model = new Model();
            model.entityUpdated += Update;
            Users = new Dictionary<int, User>();
        }

        public void JoinGame(IClientProxy proxy)
        {
            int id = Users.Count;
            Users.Add(id, new User(id, "", proxy));
            Users[id].proxy?.SendAsync("JoiningConfirmed", id);
            if (Users.Count == 2)
            {
                SendMap();
                model.solverIsAlive = true;
            }
        }

        public void SendMap()
        {
            model.WorldGeneration();
            model.GeneratePlayers(Users.Values.ToList());
            foreach (var user in Users.Values)
            {
                user.proxy?.SendAsync("CreateMap", model.Map);
            }
        }

        public void PlayerMove(DataPlayer player)
        {
            model.MovePlayerOnList(player);
        }

        public void PlayerShoot(int userId, Dictionary<string, int> shot)
        {
            model.GenerateBullet(userId, shot);
        }

        private async void Update(object? sender, EventArgs e)
        {
            Updater? up = (Updater?)sender;
            if (up != null)
            {
                foreach (var user in Users.Values)
                {
                    await user.proxy?.SendAsync("Update", up)!;
                }
            }
        }

    }
}
