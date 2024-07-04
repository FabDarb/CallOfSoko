namespace CallOfSokoClient.Class.Guns
{
    public class Glock17 : Gun
    {
        public Glock17()
        {
            Damage = 10;
            ClipSize = 12;
            Ammo = ClipSize;
            BulletSpeed = 10;
            BulletLifeTime = 2;
        }
    }
}
