namespace CallOfSokoClient.Class.UI
{
    public class LifeBar
    {
        Rectangle BorderRec { get; set; }
        Rectangle RectIn { get; set; }

        private int _PlayerLife;
        public int PlayerLife
        {
            get => _PlayerLife;
            set
            {
                _PlayerLife = value;
                RectIn = new Rectangle(0, 0, (int)WidthCalculator(value), 15);
            }
        }
        public LifeBar(int pLife)
        {
            int width = (int)WidthCalculator(pLife);
            BorderRec = new Rectangle(0, 0, 70, 15);
            RectIn = new Rectangle(0, 0, width, 15);
        }

        public void Update(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Red, RectIn);
            e.Graphics.DrawRectangle(Pens.Black, BorderRec);
        }


        private double WidthCalculator(double pLife)
        {
            return (70.0 / 100.0) * pLife;
        }
    }
}
