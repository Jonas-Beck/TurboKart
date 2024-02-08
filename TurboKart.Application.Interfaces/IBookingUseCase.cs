using TurboKart.Domain.Entities;
using TurboKart.Domain.ValueObjects;

namespace TurboKart.Application.Interfaces
{
    public interface IBookingUseCase
    {
        // TOOD Refactor BookNew optional parameter
        Task BookNew(Booking booking, IEnumerable<Booking> overlappingBookings = null);
        Task<IEnumerable<Booking>> GetAllBookings();
        Task<IEnumerable<Booking>> GetTodaysBookings();
        Task<IEnumerable<Booking>> GetWeeksBookings();
        Task<IEnumerable<Booking>> GetSpecificDateBookings(DateOnly date);
        Task<IEnumerable<Booking>> GetOverlappingBookings(DateTimeSpan bookingTime);
        Task<Booking> GetSingleBooking(object id);
        Task Update(Booking booking);
        Task Delete(object bookingId);
    }
}