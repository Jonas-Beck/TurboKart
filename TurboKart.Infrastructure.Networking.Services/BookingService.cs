using System.Collections;
using System.Net.Http.Json;
using TurboKart.Application.Interfaces;
using TurboKart.Domain.Entities;

namespace TurboKart.Infrastructure.Networking.Services
{
    public class BookingService : IBookingUseCase
    {
        private const string URL = "https://localhost:7161";

        public async Task BookNew(Booking booking, IEnumerable<Booking> overlappingBookings = null)
        {
            // Create a new instance of HttpClient using the 'using' statement for proper disposal
            using HttpClient client = new();

            // Set the base address of the HttpClient to the specified URL
            client.BaseAddress = new Uri(URL);

            // Send a POST request to the '/api/Booking/new' endpoint, including the booking information as JSON
            await client.PostAsJsonAsync("/api/Booking/new", booking);
        }

        public async Task Delete(object bookingId)
        {
            // Create a new instance of HttpClient using the 'using' statement for proper disposal
            using HttpClient client = new();

            // Set the base address of the HttpClient to the specified URL
            client.BaseAddress = new Uri(URL);

            // Send a DELETE request to the '/api/Booking/delete/{bookingId}' endpoint
            await client.DeleteAsync($"api/booking/delete/{bookingId}");
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

        public async Task<IEnumerable<Booking>> GetSpecificDateBookings(DateOnly date)
        {
            // Create a new instance of HttpClient using the 'using' statement for proper disposal
            using HttpClient client = new();

            // Set the base address of the HttpClient to the specified URL
            client.BaseAddress = new Uri(URL);

            // Send a GET request to the '/api/Booking/specific/{date}' endpoint
            // Returns a IEnumerable<Booking> with all bookings for that date
            return await client.GetFromJsonAsync<IEnumerable<Booking>>($"api/Booking/specific/{date.ToString("yyyy-M-d")}");
        }

        public async Task<IEnumerable<Booking>> GetOverlappingBookings(DateTimeSpan bookingTime)
        {
            throw new NotImplementedException();
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

        public async Task<IEnumerable<Booking>> GetWeeksBookings()
        {
            // Create a new instance of HttpClient using the 'using' statement for proper disposal
            using HttpClient client = new();

            // Set the base address of the HttpClient to the specified URL
            client.BaseAddress = new Uri(URL);

            // Send a GET request to the '/api/Booking/week' endpoint
            // Returns a IEnumerable<Booking> with all bookings for the next 7 days
            return await client.GetFromJsonAsync<IEnumerable<Booking>>("api/Booking/week");
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