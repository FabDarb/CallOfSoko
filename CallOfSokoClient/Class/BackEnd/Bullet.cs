using CallOfLibrary;
using System.Diagnostics;

namespace CallOfSokoClient
{
    public class Bullet : Block
    {
        public int IdPlayer { get; set; }
        public int Damage { get; set; } = 10;
        public int Angle { get; set; }
        public int Speed { get; set; } = 10;

        public int LifeTime { get; set; } = 2;

        private int _X;
        public new int X { get { return _X; } set { _X = value; HitBox = new Rectangle(value, Y, 5, 5); } }
        private int _Y;
        public new int Y { get { return _Y; } set { _Y = value; HitBox = new Rectangle(X, value, 5, 5); } }

        public Bullet(int x, int y, int pId, int angle)
        {
            IdPlayer = pId;
            X = x;
            Y = y;
            Angle = angle;
            HitBox = new Rectangle(X, Y, 5, 5);
        }

        public override void DrawBlock(PaintEventArgs e, PictureBox display)
        {
            Vector2 vector = new Vector2(X, Y);
            vector.Rotate(Speed, Angle);
            X = vector.X;
            Y = vector.Y;
            Debuging();
            e.Graphics.FillEllipse(Brushes.DarkGray, X, Y, 5, 5);
        }

        private void Debuging()
        {
            Debug.WriteLine($"X: {X}");
            Debug.WriteLine($"Y: {Y}");
            Debug.WriteLine($"angle: {Angle}");
        }
    }
}
