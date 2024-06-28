using CallOfLibrary;
namespace CallOfSokoClient.Class.BackEnd
{
    public class Player : Block
    {
        public int Id { get; set; }


        public Player(int x, int y, int id)
        {
            X = x;
            Y = y;
            Id = id;
            HitBox = new Rectangle(X, Y, 25, 25);
            Type = DataBlockType.Player;
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
        }
    }
}
