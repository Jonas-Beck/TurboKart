using TurboKart.Domain.Entities;

namespace TurboKart.Application.Interfaces
{
    public interface IBookingUseCase
    {
        void BookNew(DateTime dateTime, Customer customer);
        IEnumerable<Booking> GetAllBookings();
        IEnumerable<Booking> GetTodaysBookings();
    }
}