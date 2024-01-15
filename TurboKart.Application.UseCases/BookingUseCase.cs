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

        public async Task BookNew(Booking booking)
        {
            if (booking.CustomerId == 0 && booking.Customer != null)
            {
                ICustomerRepository customerRepository = unitOfWork.CustomerRepository;
                customerRepository.Save(booking.Customer);
            }

            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            bookingRepository.Save(booking);

            await unitOfWork.Commit();
        }

        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            return await bookingRepository.GetAll();
        }

        public async Task<Booking> GetSingleBooking(object id)
        {
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            return await bookingRepository.GetBy(id);
        }

        public async Task<IEnumerable<Booking>> GetTodaysBookings()
        {
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            return await bookingRepository.GetTodaysBookings();
        }

        public async Task Update(Booking booking)
        {
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            bookingRepository.Update(booking);

            await unitOfWork.Commit();
        }
        public async Task Delete(Booking booking)
        {
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            bookingRepository.Delete(booking);

            await unitOfWork.Commit();
        }
    }
}