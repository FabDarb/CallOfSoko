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
                user.proxy?.SendAsync("CreateMap", model.map);
            }
            SendPlayerList();
        }

        public void PlayerMove(DataPlayer player)
        {
            model.MovePlayerOnList(player);
            SendPlayerList();
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
