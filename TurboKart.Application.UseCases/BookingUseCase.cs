using TurboKart.Application.Interfaces;
using TurboKart.Domain.Entities;
using TurboKart.Infrastructure.Persistence.Interfaces;

namespace TurboKart.Application.UseCases
{
    public class BookingUseCase : IBookingUseCase
    {
        private readonly IUnitOfWork unitOfWork;

        public BookingUseCase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void BookNew(Booking booking)
        {
            if (booking.CustomerId == 0)
            {
                ICustomerRepository customerRepository = unitOfWork.CustomerRepository;
                customerRepository.Save(booking.Customer);
            }

            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            bookingRepository.Save(booking);

            unitOfWork.Commit();
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            return bookingRepository.GetAll();
        }

        public Booking GetSingleBooking(object id)
        {
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            return bookingRepository.GetBy(id);
        }

        public IEnumerable<Booking> GetTodaysBookings()
        {
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            return bookingRepository.GetTodaysBookings();
        }

        public void Update(Booking booking)
        {
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            bookingRepository.Update(booking);

            unitOfWork.Commit();
        }
    }
}