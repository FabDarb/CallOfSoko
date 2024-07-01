namespace CallOfSokoClient
{
    internal class Bullet : Block
    {
        public int IdPlayer { get; set; }
        public int Damage { get; set; } = 10;
        public int Angle { get; set; }
        public int Speed { get; set; } = 10;

        public Bullet(int x, int y, int id, int angle)
        {
            IdPlayer = id;
            X = x;
            Y = y;
            Angle = angle;
            HitBox = new Rectangle(X, Y, 5, 5);
        }

        public override void DrawBlock(PaintEventArgs e, PictureBox display)
        {
            Update();
            e.Graphics.FillEllipse(Brushes.DarkGray, X, Y, 5, 5);
        }

        private void Update()
        {
            CallOfSokoClient.Class.BackEnd.Vector2 v = new(X, Y);
            v.Rotate(Speed, Angle);
            X = (int)v.X;
            Y = (int)v.Y;
        }
    }
}
