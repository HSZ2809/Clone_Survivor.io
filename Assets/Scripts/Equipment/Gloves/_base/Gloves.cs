namespace ZUN
{
    public abstract class Gloves : Equipment
    {
        new public GlovesData Data { get; private set; }
        public float Atk
        {
            get
            {
                float hp = 0.0f;
                hp += Data.InitialAtk[(int)Tier];
                hp += Data.IncreaseAtkPerLevel[(int)Tier] * Level;
                return hp;
            }
        }
        public float Coefficient
        {
            get
            {
                float coefficient = 0.0f;
                for (int i = 0; i < (int)Tier; i++)
                    coefficient = Data.Coefficient[i];
                return coefficient;
            }
        }

        public Gloves(GlovesData data, EquipmentTier tier) : base(data, tier) => Data = data;
    }
}