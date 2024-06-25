using Microsoft.AspNetCore.SignalR;

namespace CallOfSokoHub
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IClientProxy? proxy { get; set; }

        public User(int id, string name, IClientProxy? proxy)
        {
            Id = id;
            Name = name;
            this.proxy = proxy;
        }

    }
}
