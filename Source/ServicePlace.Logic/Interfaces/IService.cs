namespace ServicePlace.Logic.Interfaces
{
    public interface IService<TEntity> where TEntity : class
    {
        void Create(TEntity entity);

        void Delete(TEntity entity);

        void Update(TEntity entity);

        TEntity Get(object id);
    }
}
