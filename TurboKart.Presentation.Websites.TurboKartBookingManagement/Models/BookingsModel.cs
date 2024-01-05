using TurboKart.Domain.Entities;

namespace TurboKart.Presentation.Websites.TurboKartBookingManagement.Models;

public class BookingsModel
{
    public List<Booking> Bookings { get; set; } = new List<Booking>();
    public DateOnly? Date { get; set; } = null;
    public BookingTimeFrame TimeFrame { get; set; } = BookingTimeFrame.Today;
    public string BookingTimeFrameString()
    {
        switch (TimeFrame)
        {
            case BookingTimeFrame.Today:
                return "Bookings for today";
            case BookingTimeFrame.Week:
                return "Bookings for the next 7 days";
            case BookingTimeFrame.Specific:
                return $"Bookings for {Date.ToString()}";
            default:
                return "";
        }
    }


    public enum BookingTimeFrame
    {
        Today,
        Week,
        Specific
    }
    
    
}