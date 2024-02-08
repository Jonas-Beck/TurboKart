using Microsoft.EntityFrameworkCore;
using TurboKart.Domain.Entities;
using TurboKart.Domain.ValueObjects;
using TurboKart.Infrastructure.Persistence.Interfaces;

namespace TurboKart.Infrastructure.Persistence.Repositories
{
    public class BookingRepository : RepositoriesBase<Booking>, IBookingRepository
    {
        public BookingRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Booking>> GetAll()
        {
            return await set.Include(b => b.Customer)
                .ToListAsync();
        }

        public override async Task<Booking?> GetBy(object id)
        {
            return await set.Include(b => b.Customer)
                .Where(b => b.BookingId == (int)id)
                .FirstOrDefaultAsync();
        }

        // Return bookings for today
        public async Task<IEnumerable<Booking>> GetTodaysBookings()
        {
            // Returns IEnumerable<Booking> containing all bookings with start date today
            return await set.Include(b => b.Customer)
                .Where(b => b.Time.Start.Date == DateTime.Today)
                .ToListAsync();
        }

        // Return bookings for the next 7 days
        public async Task<IEnumerable<Booking>> GetWeeksBookings()
        {
            // Get current date
            DateTime today = DateTime.Today;

            // Get date 7 days from now
            DateTime week = today.AddDays(7);

            // Return all bookings with a start date between today and week
            return await set.Include(b => b.Customer)
                .Where(b => b.Time.Start.Date >= today && b.Time.Start.Date <= week)
                .ToListAsync();
        }

        // Return bookings for specific date
        public async Task<IEnumerable<Booking>> GetSpecificDateBookings(DateOnly date)
        {
            // Returns IEnumerable<Booking> containing all bookings with start date same as parameter
            return await set.Include(b => b.Customer)
                .Where(b => b.Time.Start.Date == date.ToDateTime(TimeOnly.Parse("00:00")))
                .ToListAsync();
        }

        // Return all bookings that overlap with a specific Start and End DateTime
        public async Task<IEnumerable<Booking>> GetOverlappingBookings(DateTimeSpan bookingTime)
        {
            // Get all bookings from same day as bookingTime
            var bookings = await GetSpecificDateBookings(DateOnly.FromDateTime(bookingTime.Start));

            // Return all bookings that overlap with bookingTime
            return bookings.Where(b => DateTimeSpan.CheckOverlap(b.Time, bookingTime));
        }
    }
}