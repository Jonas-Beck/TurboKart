using System.Net.Http.Json;
using TurboKart.Application.Interfaces;
using TurboKart.Domain.Entities;

namespace TurboKart.Infrastructure.Networking.Services
{
    public class BookingService : IBookingUseCase
    {
        private const string URL = "https://localhost:7161";

        public async Task BookNew(Booking booking)
        {
            // Create a new instance of HttpClient using the 'using' statement for proper disposal
            using HttpClient client = new();
            
            // Set the base address of the HttpClient to the specified URL
            client.BaseAddress = new Uri(URL);
            
            // Send a POST request to the '/api/Booking/new' endpoint, including the booking information as JSON
            await client.PostAsJsonAsync("/api/Booking/new", booking);
        }

        public Task Delete(Booking booking)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            // Create a new instance of HttpClient using the 'using' statement for proper disposal
            using HttpClient client = new();
            
            // Set the base address of the HttpClient to the specified URL
            client.BaseAddress = new Uri(URL);
            
            // Send a GET request to the '/api/Booking/all' endpoint
            // Returns a IEnumerable<Booking> with all bookings
            return await client.GetFromJsonAsync<IEnumerable<Booking>>("api/Booking/all");
        }

        public async Task<Booking> GetSingleBooking(object id)
        {
            // Create a new instance of HttpClient using the 'using' statement for proper disposal
            using HttpClient client = new();
            
            // Set the base address of the HttpClient to the specified URL
            client.BaseAddress = new Uri(URL);
            
            // Send a GET request to the '/api/Booking/{id}' endpoint
            // Returns a Booking object with the specified ID
            return await client.GetFromJsonAsync<Booking>($"api/Booking/{id}");
        }

        public async Task<IEnumerable<Booking>> GetTodaysBookings()
        {
            // Create a new instance of HttpClient using the 'using' statement for proper disposal
            using HttpClient client = new();
            
            // Set the base address of the HttpClient to the specified URL
            client.BaseAddress = new Uri(URL);
            
            // Send a GET request to the '/api/Booking/today' endpoint
            // Returns a IEnumerable<Booking> with all bookings today
            return await client.GetFromJsonAsync<IEnumerable<Booking>>("api/Booking/today");
        }

        public async Task Update(Booking booking)
        {
            // Create a new instance of HttpClient using the 'using' statement for proper disposal
            using HttpClient client = new();
            
            // Set the base address of the HttpClient to the specified URL
            client.BaseAddress = new Uri(URL);
            
            // Send a PUT request to the '/api/Booking/update' endpoint, including the booking information as JSON
            await client.PutAsJsonAsync("/api/Booking/Update", booking);
        }
    }
}