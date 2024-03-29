using System.ComponentModel.DataAnnotations;
using TurboKart.Domain.Entities;

namespace TurboKart.Presentation.Websites.TurboKartBookingManagement.Models;

public class BookingModel
{
    
    public int BookingId { get; set; }
    
    public int CustomerId { get; set; }
    
    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public GrandprixType Type { get; set; }

    [Required]
    public int DriverCount { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public TimeOnly Time { get; set; } 
}