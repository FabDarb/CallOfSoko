namespace CallOfSokoClient.Class.UI
{
    public class RealodingBar : BarUI
    {
        public int X { get; set; }
        public int TickUpdater { get; set; }

        public int Width { get; set; }
        public RealodingBar(PictureBox display, int ammo, int realodingTotalTime)
        {
            X = display.Width;
            TickUpdater = ammo * 20 / realodingTotalTime;
        }

        public override void Update(PaintEventArgs e)
        {
            RectIn = new Rectangle(X, 15, Width, 15);
            e.Graphics.FillRectangle(Brushes.Red, RectIn);
        }
    }
}
