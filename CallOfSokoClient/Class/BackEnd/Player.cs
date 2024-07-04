using CallOfLibrary;
using CallOfSokoClient.Class.Guns;
namespace CallOfSokoClient.Class.BackEnd
{
    public class Player : Block
    {
        public int Id { get; set; }
        public int Angle { get; set; } = 0;
        public Gun Gun { get; set; }

        private int _Health;
        public int Health
        {
            get => _X;
            set
            {
                _X = value;
                ViewLifeBar?.Invoke(_X, EventArgs.Empty);
            }
        }

        private int _X;
        public new int X { get { return _X; } set { _X = value; HitBox = new Rectangle(value, Y, 25, 25); } }
        private int _Y;
        public new int Y { get { return _Y; } set { _Y = value; HitBox = new Rectangle(X, value, 25, 25); } }

        public event EventHandler? ViewLifeBar;


        public Player(int x, int y, int angle, int id, Gun gun, int health)
        {
            X = x;
            Y = y;
            Id = id;
            Angle = angle;
            Gun = gun;
            Health = health;
            HitBox = new Rectangle(X, Y, 25, 25);
            Type = DataBlockType.Player;
        }

        public Rectangle GetHitBox(int xVelocity, int yVelocity)
        {
            Rectangle hitBox = new Rectangle();
            hitBox.X = HitBox.X + xVelocity;
            hitBox.Y = HitBox.Y + yVelocity;
            hitBox.Width = HitBox.Width;
            hitBox.Height = HitBox.Height;
            return hitBox;
        }

        public override void DrawBlock(PaintEventArgs e, PictureBox display)
        {
            Brush brush;
            if (Id == 0)
            {
                brush = Brushes.Green;
            }
            else
            {
                brush = Brushes.Indigo;
            }
            e.Graphics.TranslateTransform(X + 25 / 2, Y + 25 / 2);
            e.Graphics.RotateTransform(Angle);
            e.Graphics.FillRectangle(brush, -(25 / 2), -(25 / 2), 25, 25);
            e.Graphics.ResetTransform();
            e.Graphics.FillRectangle(Brushes.Black, HitBox.X, HitBox.Y, 25, 25);
        }
    }
}
