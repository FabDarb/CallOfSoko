namespace CallOfLibrary
{
    public class Updater
    {
        public List<string> Bullets { get; set; }
        public List<DataPlayer> Players { get; set; }

        public Updater(List<DataPlayer> players, List<string> bullets)
        {
            Players = players;
            Bullets = bullets;
        }
    }
}
