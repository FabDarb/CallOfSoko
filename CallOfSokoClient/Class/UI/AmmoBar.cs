namespace CallOfSokoClient.Class.UI
{
    public class AmmoBar : BarUI
    {
        public int X { get; set; }
        List<BulletUI> Bullets { get; set; } = new List<BulletUI>();

        public int AmmoTotal { get; set; }

        public PictureBox PictureBoxDisplay { get; set; }
        public AmmoBar(int ammo, PictureBox display)
        {
            PictureBoxDisplay = display;
            AmmoTotal = ammo;
            int width = WidthCalculator(ammo);
            X = display.Width - width;
            for (int i = 0; i < ammo; i++)
            {
                Bullets.Add(new BulletUI((X + i * 20), true));
            }
        }
        public override void Update(PaintEventArgs e)
        {
            int width = WidthCalculator(AmmoTotal);
            X = PictureBoxDisplay.Width - width;
            for (int i = 0; i < AmmoTotal; i++)
            {
                Bullets[i].Update(e, X + i * 20);
            }
        }
        private int WidthCalculator(int ammo)
        {
            return 20 * ammo;
        }

        public void UpdateAmmo(int ammo)
        {
            for (int i = 0; i < AmmoTotal - ammo; i++)
            {
                Bullets[i].UpdateBrush(false);
            }
            for (int i = AmmoTotal - ammo; i < AmmoTotal; i++)
            {
                Bullets[i].UpdateBrush(true);
            }
        }
    }
}
