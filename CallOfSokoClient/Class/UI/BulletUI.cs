namespace CallOfSokoClient.Class.UI
{
    public class BulletUI
    {
        public Rectangle RectangleUI { get; set; }

        private Brush brush = Brushes.DarkGray;

        public BulletUI(int x, bool isHere)
        {
            UpdateBrush(isHere);
            RectangleUI = new Rectangle(x, 0, 10, 15);
        }

        public void Update(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(brush, RectangleUI);
        }

        public void UpdateBrush(bool isHere)
        {
            if (isHere)
            {
                brush = Brushes.DarkGray;
            }
            else
            {
                brush = Brushes.LightGray;
            }
        }
    }
}
