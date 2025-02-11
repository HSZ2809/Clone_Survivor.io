namespace ZUN
{
    public interface IMon_Damageable
    {
        float MaxHp { get; set; }

        void TakeDamage(float damage);
        void ShowDamage(float damage);
    }
}