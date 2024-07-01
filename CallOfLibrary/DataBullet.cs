namespace CallOfLibrary
{
    public class DataBullet : DataBlock
    {
        public int Id { get; set; }
        public int IdPlayer { get; set; }
        public int Damage { get; set; } = 10;
        public int Angle { get; set; }
        public int Speed { get; set; } = 10;

        public DataBullet(int id, int x, int y, int playerId, int angle)
        {
            Id = id;
            IdPlayer = playerId;
            X = x;
            Y = y;
            Angle = angle;
        }

        public override void Update()
        {
            Vector2 vector = new Vector2(X, Y);
            vector.Rotate(Speed, Angle);
            X = (int)vector.X;
            Y = (int)vector.Y;
        }
    }
}
