using TurboKart.Domain.ValueObjects;

namespace TurboKart.Domain.Entities
{
    public class Booking
    {
        public const int MinutesInGrandprixSingle = 20;
        public const int MinutesInGrandprixDouble = 30;
        
        public int BookingId { get; set; }
        public DateTimeSpan Time { get; set; }
        public int DriverCount { get; set; }
        public GrandprixType Type { get; set; }
        public Customer? Customer { get; set; }
        public int CustomerId { get; set; }

        // Custom Constructor
        public Booking(DateTime start, int driverCount, GrandprixType type, int customerId, Customer? customer = null, int bookingId = 0)
        {
            BookingId = bookingId;
            Time = new DateTimeSpan(start, GetGrandprixDuration(type));
            DriverCount = driverCount;
            Type = type;
            Customer = customer;
            CustomerId = customerId;
        }

        // Parameterless constructor for Entity Framework
        public Booking()
        {
        }

        public static TimeSpan GetGrandprixDuration(GrandprixType type)
        {
            return type switch
            {
                GrandprixType.Single => new TimeSpan(0, MinutesInGrandprixSingle, 0),
                GrandprixType.Double => new TimeSpan(0, MinutesInGrandprixDouble, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
        
    }

    public enum GrandprixType
    {
        Single,
        Double
    }
}
