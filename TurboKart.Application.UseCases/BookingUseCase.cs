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
            // Check if customer ID is not provided but customer details are provided
            if (booking.CustomerId == 0 && booking.Customer != null)
            {
                // Initialize the customer repository from the unit of work
                ICustomerRepository customerRepository = unitOfWork.CustomerRepository;
                
                // Save the customer to the database 
                await customerRepository.Save(booking.Customer);
            }

            // Initialize the booking repository from the unit of work
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            
            // Save the booking to the database 
            await bookingRepository.Save(booking);

            // Commit changes to the database 
            await unitOfWork.Commit();
        }
        
        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            // Initialize the booking repository from the unit of work
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            
            // Return IEnumerable<Booking> obtained by calling GetAll() on the booking repository
            return await bookingRepository.GetAll();
        }

        public async Task<IEnumerable<Booking>> GetSpecificDateBookings(DateOnly date)
        {
            // Initialize the booking repository from the unit of work
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            
            // Return IEnumerable<Booking> obtained by calling GetSpecificDateBookings(date) on the booking repository
            return await bookingRepository.GetSpecificDateBookings(date);
        }

        public async Task<Booking> GetSingleBooking(object id)
        {
            // Initialize the booking repository from the unit of work
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            
            // Return Booking obtained by calling GetBy(id) on the booking repository
            return await bookingRepository.GetBy(id);
        }

        public async Task<IEnumerable<Booking>> GetTodaysBookings()
        {
            // Initialize the booking repository from the unit of work
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            
            // Return IEnumerable<Booking> obtained by calling GetTodaysBookings() on the booking repository
            return await bookingRepository.GetTodaysBookings();
        }

        public async Task<IEnumerable<Booking>> GetWeeksBookings()
        {
            // Initialize the booking repository from the unit of work
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            
            // Return IEnumerable<Booking> obtained by calling GetWeeksBookings() on the booking repository
            return await bookingRepository.GetWeeksBookings();
        }

        public async Task Update(Booking booking)
        {
            // Initialize the booking repository from the unit of work
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            
            // Update the booking  
            bookingRepository.Update(booking);

            // Commit changes to the database
            await unitOfWork.Commit();
        }
        public async Task Delete(Booking booking)
        {
            // Initialize the booking repository from the unit of work
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            
            // Delete the booking
            bookingRepository.Delete(booking);

            // Commit changes to the database
            await unitOfWork.Commit();
        }
    }
}