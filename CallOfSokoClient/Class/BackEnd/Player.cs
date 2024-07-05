using CallOfLibrary;
using CallOfSokoClient.Class.Guns;
namespace CallOfSokoClient.Class.BackEnd
{
    public class Player : Block
    {
        public int Id { get; set; }
        public int Angle { get; set; } = 0;
        public Gun Gun { get; set; }
        public bool InRealoding { get; set; } = false;
        public bool IsHited { get; set; }

        public event EventHandler? IsDead;
        public Brush DefaultBrush { get; set; }
        public Brush PlayerBrush { get; set; }

        private int _Health;
        public int Health
        {
            get => _Health;
            set
            {
                _Health = value;
                ViewLifeBar?.Invoke(value, EventArgs.Empty);
                if (_Health == 0)
                {
                    IsDead?.Invoke(this, EventArgs.Empty);
                }
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
            if (Id == 0)
            {
                DefaultBrush = Brushes.Green;
            }
            else
            {
                DefaultBrush = Brushes.Indigo;
            }
            PlayerBrush = DefaultBrush;
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

            e.Graphics.TranslateTransform(X + 25 / 2, Y + 25 / 2);
            e.Graphics.RotateTransform(Angle);
            e.Graphics.FillRectangle(PlayerBrush, -(25 / 2), -(25 / 2), 25, 25);
            e.Graphics.ResetTransform();
        }
    }
}
