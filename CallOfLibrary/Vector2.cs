namespace CallOfLibrary
{
    public class Vector2
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }
        public void Rotate(int hyp, int angle)
        {
            double radian = angle * Math.PI / 180;
            X += (int)Math.Round(Math.Cos(radian) * hyp);
            Y += (int)Math.Round(Math.Sin(radian) * hyp);
        }
    }
}
