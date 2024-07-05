using CallOfLibrary;

namespace CallOfSokoHub
{
    public class Model
    {

        public const int Width = 16;
        public const int Height = 9;
        public const int Size = 100;
        public List<DataBlock> Map { get; set; }
        public List<DataPlayer> PlayerList { get; set; }
        public List<string> BulletList { get; set; }

        public event EventHandler? entityUpdated;
        public bool solverIsAlive = false;

        private Thread solver;
        private DataBlock[,] mazeWallMap;
        private Random rnd = new Random();

        public Model()
        {
            Map = new List<DataBlock>();
            PlayerList = new List<DataPlayer>();
            BulletList = new List<string>();
            solver = new Thread(new ThreadStart(Solve));
            mazeWallMap = new DataBlock[Width, Height];
            solver.Start();
        }

        public void GeneratePlayers(List<User> users)
        {
            foreach (var user in users)
            {
                bool isNotPlaced = true;

                while (isNotPlaced)
                {
                    int x = rnd.Next(1, Width);
                    int y = rnd.Next(1, Height);

                    if (mazeWallMap[x, y] == null)
                    {
                        isNotPlaced = false;
                        PlayerList.Add(new DataPlayer(user.Id, x * Size, y * Size, 0));
                    }
                }
            }
        }

        public void WorldGeneration(bool useTemplate = false)
        {
            Map.Clear();
            if (useTemplate)
            {
                UseTemplateMap();
            }
            else
            {
                mazeWallMap = new DataBlock[Width, Height];
                GenerateWorld();
            }
        }

        private void UseTemplateMap()
        {
            Map.Clear();
            Map.Add(new DataBlock(200, 50, Size, DataBlockType.Wall));
            Map.Add(new DataBlock(200, 100, Size, DataBlockType.Wall));
            Map.Add(new DataBlock(200, 150, Size, DataBlockType.Wall));
            Map.Add(new DataBlock(200, 200, Size, DataBlockType.Wall));
            Map.Add(new DataBlock(150, 200, Size, DataBlockType.Wall));
            Map.Add(new DataBlock(250, 200, Size, DataBlockType.Wall));
            Map.Add(new DataBlock(250, 250, Size, DataBlockType.Wall));
            Map.Add(new DataBlock(250, 300, Size, DataBlockType.Wall));
        }
        public void MovePlayerOnList(DataPlayer player)
        {
            DataPlayer dp = PlayerList.Where((p) => p.Id == player.Id).FirstOrDefault()!;
            dp.X = player.X;
            dp.Y = player.Y;
            dp.Angle = player.Angle;
        }

        public void GenerateBullet(int userId, Dictionary<string, int> shot)
        {
            DataPlayer? player = FindPlayerById(userId);
            if (player != null)
            {
                BulletList.Add($"{player.Id},{player.X},{player.Y},{player.Angle},{shot["Damage"]},{shot["BulletSpeed"]},{shot["BulletLifeTime"]}");
                Console.WriteLine($"angle: {player.Angle}");
            }
        }

        private void GenerateWorld()
        {
            GenerateWalls();
            GenerateBorder();
            GenerateMaze();
            EliminateWalls();
            foreach (DataBlock block in mazeWallMap)
            {
                if (block != null)
                {
                    Map.Add(block);
                }
            }
        }

        private void GenerateMaze()
        {
            //Directions 0 Down 1 Right

            DataBlock[,] workTable = new DataBlock[Width, Height];

            for (int y = 1; y < Height - 1; y++)
            {
                for (int x = 0; x < Width - 1; x++)
                {
                    if (x == 0 || y == 1 || x == Width - 1 || y == Height - 1)
                    {
                        workTable[x, y] = mazeWallMap[x, y];
                    }
                }
            }


            for (int y = 1; y < Height - 2; y++)
            {
                for (int x = 0; x < Width - 2; x++)
                {
                    if (x % 2 == 1 && y % 2 == 0)
                    {
                        int direction = rnd.Next(2);
                        int dx = MazeDirectionSetter(x, y, direction, "dx");
                        int dy = MazeDirectionSetter(x, y, direction, "dy");

                        if ((dx < 0 || dy < 1 || dx >= Width - 2 || dy >= Height - 2) || mazeWallMap[dx, dy] == workTable[dx, dy])
                        {
                            if (direction == 0)
                            {
                                direction = 1;
                            }
                            else
                            {
                                direction = 0;
                            }
                            dx = MazeDirectionSetter(x, y, direction, "dx");
                            dy = MazeDirectionSetter(x, y, direction, "dy");

                        }
                        mazeWallMap[dx, dy] = null!;
                    }
                }
            }
        }

        private int MazeDirectionSetter(int x, int y, int direction, string d)
        {
            int dx = x;
            int dy = y;


            if (direction == 0)
            {
                ++dx;
            }
            else
            {
                ++dy;
            }

            if (d == "dx")
            {
                return dx;
            }
            else
            {
                return dy;
            }
        }

        private void GenerateBorder()
        {
            for (int y = 1; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (x == 0 || y == 1 || x >= Width - 2 || y >= Height - 2)
                    {
                        if (mazeWallMap[x, y] == null)
                        {
                            mazeWallMap[x, y] = new DataBlock(x * Size, y * Size, Size, DataBlockType.Wall);
                        }
                    }
                }
            }
        }

        private void GenerateWalls()
        {
            for (int y = 1; y < Height - 1; y++)
            {
                for (int x = 0; x < Width - 1; x++)
                {
                    if (x % 2 == 0 || y % 2 == 1)
                    {
                        mazeWallMap[x, y] = new DataBlock(x * Size, y * Size, Size, DataBlockType.Wall);
                    }
                }
            }
        }

        private void EliminateWalls()
        {
            int destructionCounter = 5;
            for (int y = 2; y < Height - 1; y++)
            {
                for (int x = 1; x < Width - 1; x++)
                {
                    int destroyer = rnd.Next(destructionCounter);

                    if (destroyer <= 0)
                    {
                        mazeWallMap[x, y] = null!;
                        destructionCounter = 5;
                    }
                    --destructionCounter;
                }
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
            while (true)
            {
                if (solverIsAlive)
                {
                    Updater up;
                    up = new Updater(PlayerList, BulletList);
                    entityUpdated?.Invoke(up, EventArgs.Empty);
                    if (BulletList.Count > 0) BulletList.Clear();
                    Thread.Sleep(15);
                }
            }
        }
    }
}
