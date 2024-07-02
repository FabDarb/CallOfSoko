namespace CallOfLibrary
{
    public class DataBullet : DataBlock
    {
        public int IdPlayer { get; set; }
        public int Angle { get; set; }

        public DataBullet(int x, int y, int playerId, int angle)
        {
            IdPlayer = playerId;
            X = x;
            Y = y;
            Angle = angle;
        }
    }
}
