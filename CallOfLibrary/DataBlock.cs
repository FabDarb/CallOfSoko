namespace CallOfLibrary
{
    public enum DataBlockType
    {
        Wall,
        Player,
        Bullet,
    }
    public class DataBlock
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public DataBlockType Type { get; set; }

        public DataBlock()
        { }
        public DataBlock(int x, int y, int size, DataBlockType type)
        {
            X = x;
            Y = y;
            Size = size;
            Type = type;
        }

        //virtual public void Update() { }
    }

}
