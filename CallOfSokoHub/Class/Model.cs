using CallOfLibrary;

namespace CallOfSokoHub
{
    public class Model
    {
        public List<DataBlock> Map { get; set; }
        public List<DataPlayer> PlayerList { get; set; }
        public List<DataBlock> EntityList { get; set; }

        private Thread solver;
        private bool solverIsAlive = false;

        public Model()
        {
            Map = new List<DataBlock>();
            PlayerList = new List<DataPlayer>();
            EntityList = new List<DataBlock>();
            solver = new Thread(new ThreadStart(Solve));
            solver.Start();
        }

        public void GeneratePlayers(List<User> users)
        {
            int x = 0;
            int y = 0;
            foreach (var user in users)
            {
                PlayerList.Add(new DataPlayer(user.Id, x, y, 0));
                x += 100;
                y += 100;
            }
        }

        public void StartUp()
        {
            solverIsAlive = true;
        }
        public void EndGame()
        {
            solverIsAlive = false;
        }

        public void UseTemplateMap()
        {
            Map.Clear();
            Map.Add(new DataBlock(200, 50, DataBlockType.Wall));
            Map.Add(new DataBlock(200, 100, DataBlockType.Wall));
            Map.Add(new DataBlock(200, 150, DataBlockType.Wall));
            Map.Add(new DataBlock(200, 200, DataBlockType.Wall));
            Map.Add(new DataBlock(150, 200, DataBlockType.Wall));
            Map.Add(new DataBlock(250, 200, DataBlockType.Wall));
        }
        public void MovePlayerOnList(DataPlayer player)
        {
            DataPlayer dp = PlayerList.Where((p) => p.Id == player.Id).FirstOrDefault()!;
            dp.X = player.X;
            dp.Y = player.Y;
            dp.Angle = player.Angle;
        }

        public void GenerateBullet(int userId)
        {
            DataPlayer? player = FindPlayerById(userId);
            if (player != null)
            {
                int id = EntityList.Count;
                EntityList.Add(new DataBullet(id, player.X, player.Y, player.Id, player.Angle));
            }
        }

        private DataPlayer? FindPlayerById(int id)
        {
            foreach (DataPlayer player in PlayerList)
            {
                if (player.Id == id)
                {
                    return player;
                }
            }
            return null;
        }

        private void Solve()
        {
            while (solverIsAlive)
            {
                foreach (DataBlock entity in EntityList)
                {
                    entity.Update();
                }
            }
        }
    }
}
