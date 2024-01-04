using TurboKart.Domain.Entities;

namespace TurboKart.Application.Interfaces
{
    public interface IBookingUseCase
    {
        void BookNew(Booking booking);
        Task<IEnumerable<Booking>> GetAllBookings();
        Task<IEnumerable<Booking>> GetTodaysBookings();
        Task<Booking> GetSingleBooking(object id);
        void Update(Booking booking);
        void Delete(Booking booking);
    }
}