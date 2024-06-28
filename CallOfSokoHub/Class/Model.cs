using CallOfLibrary;

namespace CallOfSokoHub
{
    public class Model
    {
        public List<DataBlock> map { get; set; }
        public List<DataPlayer> PlayerList { get; set; }

        public Model()
        {
            map = new List<DataBlock>();
            PlayerList = new List<DataPlayer>();
        }

        public void GeneratePlayers(List<User> users)
        {
            int x = 0;
            int y = 0;
            foreach (var user in users)
            {
                PlayerList.Add(new DataPlayer(user.Id, x, y));
                x += 100;
                y += 100;
            }
        }

        public void UseTemplateMap()
        {
            map.Clear();
            map.Add(new DataBlock(200, 50, DataBlockType.Wall));
            map.Add(new DataBlock(200, 100, DataBlockType.Wall));
            map.Add(new DataBlock(200, 150, DataBlockType.Wall));
            map.Add(new DataBlock(200, 200, DataBlockType.Wall));
            map.Add(new DataBlock(150, 200, DataBlockType.Wall));
            map.Add(new DataBlock(250, 200, DataBlockType.Wall));
        }
        public void MovePlayerOnList(DataPlayer player)
        {
            DataPlayer dp = PlayerList.Where((p) => p.Id == player.Id).FirstOrDefault()!;
            dp.X = player.X;
            dp.Y = player.Y;
        }
    }
}
