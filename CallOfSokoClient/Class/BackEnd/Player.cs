using CallOfLibrary;
namespace CallOfSokoClient.Class.BackEnd
{
    public class Player : Block
    {
        public int Id { get; set; }
        public int Angle { get; set; } = 0;


        public Player(int x, int y, int angle, int id)
        {
            X = x;
            Y = y;
            Id = id;
            Angle = angle;
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
            e.Graphics.TranslateTransform(X + 25 / 2, Y + 25 / 2);
            e.Graphics.RotateTransform(Angle);
            e.Graphics.FillRectangle(brush, -(25 / 2), -(25 / 2), 25, 25);
            e.Graphics.ResetTransform();
        }
    }
}
