using Microsoft.EntityFrameworkCore;

using TurboKart.Domain.Entities;
using TurboKart.Infrastructure.Persistence.Interfaces;

namespace TurboKart.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : RepositoriesBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext dbContext) : base(dbContext)
        {

        }

    }
}
