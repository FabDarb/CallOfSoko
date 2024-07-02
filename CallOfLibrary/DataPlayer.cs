namespace CallOfLibrary
{
    public class DataPlayer : DataBlock
    {
        public int Id { get; set; }

        public int Angle { get; set; }

        public DataPlayer(int id, int x, int y, int angle)
        {
            Id = id;
            X = x;
            Y = y;
            Angle = angle;
            Type = DataBlockType.Player;
        }

        //public override void Update()
        //{

        //}
    }
}
