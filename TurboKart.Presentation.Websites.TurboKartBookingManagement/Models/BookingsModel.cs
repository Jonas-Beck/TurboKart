using TurboKart.Domain.Entities;

namespace TurboKart.Presentation.Websites.TurboKartBookingManagement.Models;

/// <summary>
/// Represents a model for managing multiple bookings.
/// </summary>
public class BookingsModel
{
    /// <summary>
    /// Gets or sets the IEnumerable of bookings associated with this model.
    /// Default value is Empty List of Booking
    /// </summary>
    public IEnumerable<Booking> Bookings { get; set; } = new List<Booking>();
    
    /// <summary>
    /// Gets or sets the date for which bookings are being managed. Can be null for certain time frames.
    /// </summary>
    public DateOnly? Date { get; set; }

    /// <summary>
    /// Gets or sets the time frame for which bookings are filtered.
    /// Default value is set to BookingTimeFrame.Today.
    /// </summary>
    public BookingTimeFrame TimeFrame { get; set; } = BookingTimeFrame.Today;

    /// <summary>
    /// Generates a human-readable string representation of the current booking time frame.
    /// </summary>
    /// <returns>A string describing the booking time frame.</returns>
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

    /// <summary>
    /// Represents different time frames for filtering bookings.
    /// </summary>
    public enum BookingTimeFrame
    {
        /// <summary>
        /// Bookings for the current day.
        /// </summary>
        Today,

        /// <summary>
        /// Bookings for the next 7 days.
        /// </summary>
        Week,

        /// <summary>
        /// Bookings for a specific date.
        /// </summary>
        Specific,
    }
}