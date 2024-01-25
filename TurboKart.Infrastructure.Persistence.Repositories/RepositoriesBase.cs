using Microsoft.EntityFrameworkCore;
using TurboKart.Infrastructure.Persistence.Interfaces;

namespace TurboKart.Infrastructure.Persistence.Repositories
{
    public abstract class RepositoriesBase<T> : IRepository<T> where T : class
    {
        protected DbSet<T> set;

        protected RepositoriesBase(DbContext dbContext)
        {
            this.set = dbContext.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await set.ToListAsync();
        }

        public virtual async Task<T?> GetBy(object id)
        {
            return await set.FindAsync(id);
        }

        public async Task Save(T entity)
        {
            await set.AddAsync(entity);
        }

        public void Update(T entity)
        {
             set.Update(entity);
        }

        public void Delete(T entity)
        {
            set.Remove(entity);
        }
    }
}