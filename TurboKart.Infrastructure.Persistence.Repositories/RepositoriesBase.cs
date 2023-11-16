using Microsoft.EntityFrameworkCore;
using TurboKart.Infrastructure.Persistence.Interfaces;

namespace TurboKart.Infrastructure.Persistence.Repositories
{
    public abstract class RepositoriesBase<T> : IRepository<T> where T : class
    {
        private DbSet<T> set;

        public RepositoriesBase(DbContext dbContext)
        {
            this.set = dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return set.ToList();
        }

        public T GetBy(object id)
        {
            return set.Find(id);
        }

        public void Save(T entity)
        {
            set.Add(entity);
        }
    }
}