using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TurboKart.Domain.Entities;
using TurboKart.Domain.ValueObjects;

namespace TurboKart.Infrastructure.Persistence.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetTodaysBookings();
        Task<IEnumerable<Booking>> GetWeeksBookings();
        Task<IEnumerable<Booking>> GetSpecificDateBookings(DateOnly date);
        Task<IEnumerable<Booking>> GetOverlappingBookings(DateTimeSpan bookingTime);

    }
}