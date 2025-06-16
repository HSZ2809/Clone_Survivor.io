namespace ZUN
{
    public interface IDamageable
    {
        bool TryTakeDamage(float damage);
        void ShowDamage(float damage);
    }
}
