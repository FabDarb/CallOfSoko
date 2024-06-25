namespace CallOfSokoClient.Class.BackEnd
{
    public class Wall : Block
    {

        public Wall(int x, int y)
        {
            X = x;
            Y = y;
            HitBox = new Rectangle(X, Y, 50, 50);
        }
        public override void DrawBlock(PaintEventArgs e, PictureBox display)
        {
            e.Graphics.FillRectangle(Brushes.Brown, HitBox);
        }
    }
}
