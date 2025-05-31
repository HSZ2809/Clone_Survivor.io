namespace ZUN
{
    public abstract class Necklace : Equipment
    {
        new public NecklaceData Data { get; private set; }
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

        public Necklace(NecklaceData data, EquipmentTier tier) : base(data, tier) => Data = data;
    }
}