namespace CallOfSokoClient.Class.BackEnd
{
    public class Vector2
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }
        public void Rotate(int hyp, int angle)
        {
            X += Math.Sin((double)angle) * hyp;
            Y += Math.Cos((double)angle) * hyp;
        }
    }
}
