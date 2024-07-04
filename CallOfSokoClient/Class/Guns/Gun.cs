namespace CallOfSokoClient.Class.Guns
{
    public abstract class Gun
    {
        public int Damage { get; set; }
        public int ClipSize { get; set; }
        public int Ammo { get; set; }
        public int BulletSpeed { get; set; }
        public int BulletLifeTime { get; set; }

        public Dictionary<string, int>? Shoot()
        {
            if (TestIfShoot())
            {
                --Ammo;
                return new Dictionary<string, int>() { { "Damage", Damage }, { "BulletSpeed", BulletSpeed }, { "BulletLifeTime", BulletLifeTime } };
            }
            return null;
        }

        public bool TestIfShoot()
        {
            if (Ammo >= 0) return true;
            return false;
        }
        public void Reload()
        {
            Ammo = ClipSize;
        }
    }
}
