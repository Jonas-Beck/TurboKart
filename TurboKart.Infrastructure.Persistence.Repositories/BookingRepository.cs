using Microsoft.EntityFrameworkCore;
using TurboKart.Domain.Entities;
using TurboKart.Infrastructure.Persistence.Interfaces;

namespace TurboKart.Infrastructure.Persistence.Repositories
{
    public class BookingRepository : RepositoriesBase<Booking>, IBookingRepository
    {
        public BookingRepository(DbContext dbContext) : base(dbContext)
        {

        }

        // Return bookings for today
        public async Task<IEnumerable<Booking>> GetTodaysBookings()
        {
            // Returns IEnumerable<Booking> containing all bookings with start date today
            return await set.Where(b => b.Start.Date == DateTime.Today).ToListAsync();
        }

        // Return bookings for the next 7 days
        public async Task<IEnumerable<Booking>> GetWeeksBookings()
        {
            // Get current date
            DateTime today = DateTime.Today;
            
            // Get date 7 days from now
            DateTime week = today.AddDays(7);

            // Return all bookings with a start date between today and week
            return await set.Where(b => b.Start.Date >= today && b.Start.Date <= week).ToListAsync();
        }

        // Return bookings for specific date
        public async Task<IEnumerable<Booking>> GetSpecificDateBookings(DateOnly date)
        {
            // Returns IEnumerable<Booking> containing all bookings with start date same as parameter
            return await set.Where(b => b.Start.Date == date.ToDateTime(TimeOnly.Parse("00:00"))).ToListAsync();
        }
    }
}
