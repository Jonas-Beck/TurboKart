using TurboKart.Domain.Entities;

namespace TurboKart.Application.Interfaces
{
    public interface IBookingUseCase
    {
        void BookNew(Booking booking, Customer customer);
        IEnumerable<Booking> GetAllBookings();
        IEnumerable<Booking> GetTodaysBookings();
        Booking GetSingleBooking(object id);
    }
}