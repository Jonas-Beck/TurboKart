using TurboKart.Domain.Entities;

namespace TurboKart.Application.Interfaces
{
    public interface IBookingUseCase
    {
        void BookNew(Booking booking);
        IEnumerable<Booking> GetAllBookings();
        IEnumerable<Booking> GetTodaysBookings();
        Booking GetSingleBooking(object id);
        void Update(Booking booking);
        void Delete(Booking booking);
    }
}