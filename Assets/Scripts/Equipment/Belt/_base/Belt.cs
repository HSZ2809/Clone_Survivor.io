namespace ZUN
{
    public abstract class Belt : Equipment
    {
        new public BeltData Data { get; private set; }
        public float Hp
        {
            get
            {
                float hp = 0.0f;
                hp += Data.InitialHp[(int)Tier];
                hp += Data.IncreaseHpPerLevel[(int)Tier] * Level;
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

        public Belt(BeltData data, EquipmentTier tier) : base(data, tier) => Data = data;
    }
}