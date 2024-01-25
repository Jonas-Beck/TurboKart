namespace TurboKart.Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime Start { get; set; }
        public int DriverCount { get; set; }
        public GrandprixType Type { get; set; }
        public Customer? Customer { get; set; }
        public int CustomerId { get; set; }
    }

    public enum GrandprixType
    {
        Single,
        Double
    }
}
