namespace TurboKart.Infrastructure.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();

        IBookingRepository BookingRepository { get; }
        ICustomerRepository CustomerRepository { get; }

    }
}