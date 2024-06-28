
namespace CallOfSokoClient.Class.BackEnd
{
    public class Ball : Block
    {
        public int IdPlayer { get; set; }
        public int Damage { get; set; } = 10;

        public int Speed { get; set; } = 10;

        public Ball(int x, int y, int id)
        {
            IdPlayer = id;
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
