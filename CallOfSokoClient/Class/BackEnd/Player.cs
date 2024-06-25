
namespace CallOfSokoClient.Class.BackEnd
{
    public class Player : Block
    {
        public Player(int x, int y)
        {
            X = x;
            Y = y;
            HitBox = new Rectangle(X, Y, 25, 25);
        }

        public override void DrawBlock(PaintEventArgs e, PictureBox display)
        {
            e.Graphics.FillRectangle(Brushes.Yellow, HitBox);
        }
    }
}
