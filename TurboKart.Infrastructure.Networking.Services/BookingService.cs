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

        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            using HttpClient client = new();
            client.BaseAddress = new Uri(URL);
            return await client.GetFromJsonAsync<IEnumerable<Booking>>("api/Booking/all");
        }

        public async Task<Booking> GetSingleBooking(object id)
        {
            using HttpClient client = new();
            client.BaseAddress = new Uri(URL);
            return await client.GetFromJsonAsync<Booking>($"api/Booking/{id}");
        }

        public async Task<IEnumerable<Booking>> GetTodaysBookings()
        {
            using HttpClient client = new();
            client.BaseAddress = new Uri(URL);
            return await client.GetFromJsonAsync<IEnumerable<Booking>>("api/Booking/today");
        }

        public async void Update(Booking booking)
        {
            using HttpClient client = new();
            client.BaseAddress = new Uri(URL);
            await client.PutAsJsonAsync("/api/Booking/Update", booking);
        }
    }
}