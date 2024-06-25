namespace CallOfLibrary
{
    internal class DataPlayer : DataBlock
    {
        public int Id { get; set; }

        public DataPlayer(int id, int x, int y)
        {
            Id = id;
            X = x;
            Y = y;
            Type = DataBlockType.Player;
        }
    }
}
