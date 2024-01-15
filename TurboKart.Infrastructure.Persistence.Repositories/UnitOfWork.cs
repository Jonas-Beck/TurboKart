using Microsoft.EntityFrameworkCore;

using TurboKart.Infrastructure.Persistence.Interfaces;

namespace TurboKart.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext dbContext;
        private IBookingRepository bookingRepository;
        private ICustomerRepository customerRepository;

        public UnitOfWork(DbContext dbContext, IBookingRepository bookingRepository, ICustomerRepository customerRepository)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public IBookingRepository BookingRepository => bookingRepository;

        public ICustomerRepository CustomerRepository => customerRepository;

        public async Task Commit()
        {
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new UnitOfWorkException("Unable to commit transaction.", e);
            }
        }

        public async Task Dispose()
        {
            try
            {
                await dbContext.DisposeAsync();
            }
            catch (Exception e)
            {
                throw new UnitOfWorkException("Unable to commit transaction.", e);
            }
            finally
            {
                dbContext = null;
            }
        }

        public Task Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
