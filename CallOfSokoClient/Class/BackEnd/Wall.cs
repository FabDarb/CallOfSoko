using CallOfLibrary;

namespace CallOfSokoClient.Class.BackEnd
{
    public class Wall : Block
    {

        public Wall(int x, int y, int size)
        {
            X = x;
            Y = y;
            HitBox = new Rectangle(X, Y, size, size);
            Type = DataBlockType.Wall;
        }
        public override void DrawBlock(PaintEventArgs e, PictureBox display)
        {
            e.Graphics.FillRectangle(Brushes.SandyBrown, HitBox);
        }
    }
}
