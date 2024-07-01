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
            model.entityUpdated += UpdateShoot;
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
            }
        }

        public void SendMap()
        {
            model.UseTemplateMap();
            model.GeneratePlayers(Users.Values.ToList());
            foreach (var user in Users.Values)
            {
                user.proxy?.SendAsync("CreateMap", model.Map);
            }
            SendPlayerList();
        }

        public void PlayerMove(DataPlayer player)
        {
            model.MovePlayerOnList(player);
            SendPlayerList();
        }

        public void PlayerShoot(int userId)
        {
            model.GenerateBullet(userId);
        }

        private void UpdateShoot(object? sender, EventArgs e)
        {
            foreach (var user in Users.Values)
            {
                user.proxy?.SendAsync("UpdateShoot", model.BulletList);
            }
        }

        private void SendPlayerList()
        {
            foreach (var user in Users.Values)
            {
                user.proxy?.SendAsync("UpdatePossitionPlayer", model.PlayerList);
            }
        }

    }
}
