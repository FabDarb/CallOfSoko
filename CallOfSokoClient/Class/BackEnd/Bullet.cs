namespace CallOfSokoClient
{
    public class Bullet : Block
    {
        public int Id { get; set; }
        public int IdPlayer { get; set; }
        public int Damage { get; set; } = 10;

        private int _X;
        public new int X { get { return _X; } set { _X = value; HitBox = new Rectangle(value, Y, 5, 5); } }
        private int _Y;
        public new int Y { get { return _Y; } set { _Y = value; HitBox = new Rectangle(X, value, 5, 5); } }

        public Bullet(int x, int y, int pId, int id)
        {
            IdPlayer = pId;
            Id = id;
            X = x;
            Y = y;
            HitBox = new Rectangle(X, Y, 5, 5);
        }

        public override void DrawBlock(PaintEventArgs e, PictureBox display)
        {
            e.Graphics.FillEllipse(Brushes.DarkGray, X, Y, 5, 5);
        }
    }
}
