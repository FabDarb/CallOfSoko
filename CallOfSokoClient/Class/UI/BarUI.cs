namespace CallOfSokoClient.Class.UI
{
    public abstract class BarUI
    {
        public Rectangle BorderRec { get; set; }
        public Rectangle RectIn { get; set; }

        public abstract void Update(PaintEventArgs e);
    }
}
