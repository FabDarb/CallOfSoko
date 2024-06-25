namespace CallOfSokoClient.Class.BackEnd
{
    public abstract class Block
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Rectangle HitBox { get; set; }

        public abstract void DrawBlock(PaintEventArgs e, PictureBox display);
    }
}
