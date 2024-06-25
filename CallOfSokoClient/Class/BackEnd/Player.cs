using CallOfLibrary;

namespace CallOfSokoClient.Class.BackEnd
{
    public class Player : Block
    {
        public int Id { get; set; }
        public Player(int x, int y)
        {
            X = x;
            Y = y;
            HitBox = new Rectangle(X, Y, 25, 25);
            Type = DataBlockType.Player;
        }

        public override void DrawBlock(PaintEventArgs e, PictureBox display)
        {
            e.Graphics.FillRectangle(Brushes.Yellow, HitBox);
        }
    }
}
