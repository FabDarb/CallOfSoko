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
        }

        public void SendMap()
        {
            model.UseTemplateMap();
            model.GeneratePlayers(Users.Values.ToList());
            Clients?.All.SendAsync("UpdateGame", model.map);
        }
    }
}
