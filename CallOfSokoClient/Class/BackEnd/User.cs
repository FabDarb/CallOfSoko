namespace CallOfSokoClient.Class.BackEnd
{
    public class User
    {
        public int UserId { get; set; }
        public int IsMoving { get; set; } = 0;
        public int VerticalVelocity { get; set; } = 0;
        public int HorizontalVelocity { get; set; } = 0;

        public Dictionary<Keys, bool> MovementInput { get; set; }

        public User()
        {
            MovementInput = new Dictionary<Keys, bool>();
            MovementInput.Add(Keys.W, false);
            MovementInput.Add(Keys.D, false);
            MovementInput.Add(Keys.S, false);
            MovementInput.Add(Keys.A, false);
        }

    }
}
