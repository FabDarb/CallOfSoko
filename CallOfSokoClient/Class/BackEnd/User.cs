namespace CallOfSokoClient.Class.BackEnd
{
    public class User
    {
        public int UserId { get; set; }
        public int IsMoving { get; set; } = 0;

        public Dictionary<Keys, MovementInputVelocity> MovementInput { get; set; }

        public User()
        {
            MovementInput = new Dictionary<Keys, MovementInputVelocity>();
            MovementInput.Add(Keys.W, new MovementInputVelocity());
            MovementInput.Add(Keys.D, new MovementInputVelocity());
            MovementInput.Add(Keys.S, new MovementInputVelocity());
            MovementInput.Add(Keys.A, new MovementInputVelocity());
        }

    }
}
