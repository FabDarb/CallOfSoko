using CallOfLibrary;

namespace CallOfSokoClient.Class.BackEnd
{
    public class Map
    {
        public List<Block> MapDisplay = new List<Block>();
        public Player? ActualPlayer { get; set; }
        public List<Player> PlayerList { get; set; } = new List<Player>();

        static public Map Instance { get; } = new Map();

        public bool IsInit { get; set; } = false;


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
                    Player p = PlayerList.Where((player) => player.Id == dataplayer.Id).First()!;
                    p.X = dataplayer.X;
                    p.Y = dataplayer.Y;
                }
            }
            else
            {
                foreach (DataPlayer dataplayer in dataplayers)
                {
                    Player p = new Player(dataplayer.X, dataplayer.Y, dataplayer.Id);
                    PlayerList.Add(p);
                    MapDisplay.Add(p);
                    if (p.Id == MyUser.UserId) ActualPlayer = p;
                }
            }
        }

        public void PlayerMove(User MyUser)
        {
            foreach (Keys input in MyUser.MovementInput.Keys)
            {
                switch (input)
                {
                    case Keys.W:
                        if (MyUser.MovementInput[input]) --ActualPlayer!.Y;
                        break;
                    case Keys.S:
                        if (MyUser.MovementInput[input]) ++ActualPlayer!.Y;
                        break;
                    case Keys.D:
                        if (MyUser.MovementInput[input]) ++ActualPlayer!.X;
                        break;
                    case Keys.A:
                        if (MyUser.MovementInput[input]) --ActualPlayer!.X;
                        break;
                }
            }
        }
    }
}
