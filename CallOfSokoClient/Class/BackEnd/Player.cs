using CallOfLibrary;
namespace CallOfSokoClient.Class.BackEnd
{
    public class Player : Block
    {
        public int Id { get; set; }

        public Rectangle? MoveViewPlayer;

        public int RecX { get; set; }

        public int RecY { get; set; }

        private int _X;

        public new int X
        {
            set
            {
                _X = value;
                RecX = value - 50;
            }
            get { return _X; }
        }
        private int _Y;
        public new int Y
        {
            set
            {
                _Y = value;
                RecY = value - 50;
            }
            get { return _Y; }
        }
        public Player(int x, int y, int id, int myUserId)
        {
            X = x;
            Y = y;
            Id = id;
            HitBox = new Rectangle(X, Y, 25, 25);
            Type = DataBlockType.Player;
            if (myUserId == id)
            {
                MoveViewPlayer = new Rectangle(RecX, RecY, 125, 125);
            }
        }

        public override void DrawBlock(PaintEventArgs e, PictureBox display)
        {
            if (MoveViewPlayer != null)
            {
                MoveViewPlayer = new Rectangle(RecX, RecY, 125, 125);
                Brush newB = new SolidBrush(Color.FromArgb(50, 200, 200, 200));
                e.Graphics.FillEllipse(newB, MoveViewPlayer.Value);
            }
            Brush brush;
            if (Id == 0)
            {
                brush = Brushes.Green;
            }
            else
            {
                brush = Brushes.Indigo;
            }
            e.Graphics.FillRectangle(brush, X, Y, 25, 25);
        }
    }
}
