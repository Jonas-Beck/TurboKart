using TurboKart.Domain.Entities;

namespace TurboKart.Application.Interfaces
{
    public interface IBookingUseCase
    {
        Task BookNew(Booking booking);
        Task<IEnumerable<Booking>> GetAllBookings();
        Task<IEnumerable<Booking>> GetTodaysBookings();
        Task<Booking> GetSingleBooking(object id);
        Task Update(Booking booking);
        Task Delete(Booking booking);
    }
}