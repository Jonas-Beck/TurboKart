﻿using Microsoft.EntityFrameworkCore;
using TurboKart.Domain.Entities;
using TurboKart.Infrastructure.Persistence.Interfaces;

namespace TurboKart.Infrastructure.Persistence.Repositories
{
    public class BookingRepository : RepositoriesBase<Booking>, IBookingRepository
    {
        public BookingRepository(DbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Booking>> GetTodaysBookings()
        {
            return await set.Where(b => b.Start.Date == DateTime.Today).ToListAsync();
        }
    }
}
