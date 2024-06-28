using CallOfLibrary;
namespace CallOfSokoClient.Class.BackEnd
{
    public class Player : Block
    {
        public int Id { get; set; }

        public Rectangle? MoveViewPlayer;

        public int? RecX { get; set; }

        public int? RecY { get; set; }

        private int _X;

        public new int X
        {
            set
            {
                if (RecX != null)
                {
                    RecX = _X - 50;
                }
                _X = value;
            }
            get { return _X; }
        }
        private int _Y;
        public new int Y
        {
            set
            {
                if (RecY != null)
                {
                    RecY = _Y - 50;
                }
                _Y = value;
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
                MoveViewPlayer = new Rectangle(RecX!.Value, RecY!.Value - 50, 125, 125);
            }
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
            e.Graphics.FillRectangle(brush, X, Y, 25, 25);
            if (MoveViewPlayer != null)
            {
                e.Graphics.FillEllipse(Brushes.LightGray, MoveViewPlayer.Value);
            }
        }
    }
}
