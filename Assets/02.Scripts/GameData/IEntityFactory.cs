namespace ZUN
{
    public interface IEntityFactory<TInfo, TEntity>
    {
        bool TryCreate(TInfo info, out TEntity entity);
    }
}