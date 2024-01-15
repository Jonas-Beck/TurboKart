namespace TurboKart.Infrastructure.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
        Task Rollback();

        IBookingRepository BookingRepository { get; }
        ICustomerRepository CustomerRepository { get; }

    }
}