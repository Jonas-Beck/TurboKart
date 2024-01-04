using System.Net.Http.Json;
using TurboKart.Application.Interfaces;
using TurboKart.Domain.Entities;

namespace TurboKart.Infrastructure.Networking.Services
{
    public class BookingService : IBookingUseCase
    {
        private const string URL = "https://localhost:7161";

        public async void BookNew(Booking booking)
        {
            using HttpClient client = new();
            client.BaseAddress = new Uri(URL);
            await client.PostAsJsonAsync("/api/Booking/new", booking);
        }

        public void Delete(Booking booking)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Booking>> GetAllBookings()
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetSingleBooking(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Booking>> GetTodaysBookings()
        {
            throw new NotImplementedException();
        }

        public void Update(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}