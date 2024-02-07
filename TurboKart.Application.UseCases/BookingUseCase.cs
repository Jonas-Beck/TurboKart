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

        public async Task BookNew(Booking newBooking, IEnumerable<Booking> overlappingBookings)
        {
            // Check new booking driverCount throw exception if not valid
            if (newBooking.DriverCount is > 20 or <= 0)
                throw new ArgumentException("Driver count needs to be between 1-20");

            // Check if new booking has any overlaps
            if (overlappingBookings.Any())
            {
                // Add new booking to list with overlap bookings
                overlappingBookings = overlappingBookings.Append(newBooking);

                // Loop list of all bookings that overlap with the new booking
                foreach (Booking booking  in overlappingBookings)
                {
                    // Get total driverCount of all bookings that overlap with the current booking from loop
                    var totalDriverCount = overlappingBookings
                        .Where(b => DateTimeSpan.CheckOverlap(booking.Time, b.Time))
                        .Aggregate(0, (total, next) => total + next.DriverCount);

                    // If totalDriverCount > 20 the new booking cannot be created 
                    if (totalDriverCount > 20)
                        throw new ArgumentException("Not enough space on the track");
                }
            }
            
            // Check if customer ID is not provided but customer details are provided
            if (newBooking.CustomerId == 0 && newBooking.Customer != null)
            {
                // Initialize the customer repository from the unit of work
                ICustomerRepository customerRepository = unitOfWork.CustomerRepository;
                
                // Save the customer to the database 
                await customerRepository.Save(newBooking.Customer);
            }

            // Initialize the booking repository from the unit of work
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;
            
            // Save the booking to the database 
            await bookingRepository.Save(newBooking);

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

        public async Task<IEnumerable<Booking>> GetOverlappingBookings(DateTimeSpan bookingTime)
        {
            // Initialize the booking repository from the unit of work
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;

            // Return IEnumerable<Booking> obtained by calling GetOverlappingBookings(start, end) on the booking repository
            return await bookingRepository.GetOverlappingBookings(bookingTime);
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
        public async Task Delete(object bookingId)
        {
            // Initialize the booking repository from the unit of work
            IBookingRepository bookingRepository = unitOfWork.BookingRepository;

            // Get the booking object using GetBy 
            Booking booking = await bookingRepository.GetBy(bookingId);
            
            // Delete the booking
            bookingRepository.Delete(booking);

            // Commit changes to the database
            await unitOfWork.Commit();
        }
    }
}