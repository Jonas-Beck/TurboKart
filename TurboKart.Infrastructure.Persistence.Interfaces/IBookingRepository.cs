using TurboKart.Domain.Entities;

namespace TurboKart.Infrastructure.Persistence.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        IEnumerable<Booking> GetTodaysBookings();

    }
}