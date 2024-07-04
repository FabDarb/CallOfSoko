using CallOfLibrary;
using CallOfSokoClient.Class.Guns;

namespace CallOfSokoClient.Class.BackEnd
{
    public class Map
    {
        public List<Block> MapDisplay = new List<Block>();
        public Player? ActualPlayer { get; set; }
        public List<Player> PlayerList { get; set; } = new List<Player>();

        public List<Bullet> BulletList { get; set; } = new List<Bullet>();

        static public Map Instance { get; } = new Map();

        public bool IsInit { get; set; } = false;
        public event EventHandler? UpdateLife;


        public void CreateMap(List<DataBlock> datablocks)
        {
            MapDisplay.Clear();
            foreach (DataBlock datablock in datablocks)
            {
                switch (datablock.Type)
                {
                    case DataBlockType.Wall:
                        MapDisplay.Add(new Wall(datablock.X, datablock.Y));
                        break;
                }
            }
        }
        public void UpdateMap(List<DataPlayer> dataplayers, User MyUser)
        {
            if (IsInit)
            {
                foreach (DataPlayer dataplayer in dataplayers)
                {
                    if (dataplayer.Id != MyUser.UserId)
                    {
                        Player p = PlayerList.Where((player) => player.Id == dataplayer.Id).First()!;
                        p.X = dataplayer.X;
                        p.Y = dataplayer.Y;
                        p.Angle = dataplayer.Angle;
                    }
                }
            }
            else
            {
                foreach (DataPlayer dataplayer in dataplayers)
                {
                    Player p = new Player(dataplayer.X, dataplayer.Y, dataplayer.Angle, dataplayer.Id, new Glock17(), 100);
                    PlayerList.Add(p);
                    MapDisplay.Add(p);
                    if (p.Id == MyUser.UserId) ActualPlayer = p;
                    p.ViewLifeBar += P_ViewLifeBar;
                }
            }
        }

        private void P_ViewLifeBar(object? sender, EventArgs e)
        {
            UpdateLife?.Invoke(sender, EventArgs.Empty);
        }

        public void UpdateShoot(List<string> dataBullets)
        {
            foreach (string databullet in dataBullets)
            {
                int[] data = CustomTryParse(databullet.Split(','));
                Bullet b = new Bullet(data[1], data[2], data[0], data[3], data[4], data[5], data[6]);
                BulletList.Add(b);
                MapDisplay.Add(b);
            }
        }

        public void PlayerMove(User MyUser)
        {
            ActualPlayer!.X += MyUser.HorizontalVelocity;
            ActualPlayer!.Y += MyUser.VerticalVelocity;
        }
        private int[] CustomTryParse(string[] data)
        {
            int[] tab = new int[data.Length];
            int i = 0;
            foreach (string s in data)
            {
                tab[i] = int.Parse(s);
                i++;
            }
            return tab;
        }

        public void RemoveBullet(Bullet b)
        {
            BulletList.Remove(b);
            MapDisplay?.Remove(b);
        }
    }
}