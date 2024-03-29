﻿using TurboKart.Domain.Entities;

namespace TurboKart.Infrastructure.Persistence.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetTodaysBookings();
        Task<IEnumerable<Booking>> GetWeeksBookings();
        Task<IEnumerable<Booking>> GetSpecificDateBookings(DateOnly date);

    }
}