namespace CallOfSokoClient.Class.Guns
{
    public class Glock17 : Gun
    {
        public Glock17()
        {
            Damage = 10;
            ClipSize = 12;
            Ammo = ClipSize;
            BulletSpeed = 15;
            BulletLifeTime = 2;
            RealodingTime = 4;
            InRealoding = RealodingTime;
        }
    }
}
