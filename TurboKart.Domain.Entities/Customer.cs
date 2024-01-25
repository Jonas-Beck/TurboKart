using System.Text.Json.Serialization;

namespace TurboKart.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Booking>? Bookings { get; set; }
    }
}
