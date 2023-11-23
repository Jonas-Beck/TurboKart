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

        public void BookNew(Booking booking, Customer customer)
        {
            booking.Customer = customer;

            if (customer.CustomerId == 0)
            {
                ICustomerRepository customerRepository = unitOfWork.CustomerRepository;
                customerRepository.Save(customer);
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
    }
}