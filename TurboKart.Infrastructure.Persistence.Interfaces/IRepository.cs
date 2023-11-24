namespace TurboKart.Infrastructure.Persistence.Interfaces
{
    public interface IRepository<T>
    {
        void Save(T entity);
        IEnumerable<T> GetAll();
        T GetBy(object id);
        void Update(T entity);



    }
}