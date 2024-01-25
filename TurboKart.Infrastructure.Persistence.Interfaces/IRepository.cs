namespace TurboKart.Infrastructure.Persistence.Interfaces
{
    public interface IRepository<T>
    {
        Task Save(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetBy(object id);
        void Update(T entity);
        void Delete(T entity);



    }
}