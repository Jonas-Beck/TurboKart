using System.ComponentModel.DataAnnotations;

namespace TurboKart.Presentation.Websites.TurboKartBookingManagement.Models;

public class NewBookingModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public double PhoneNumber { get; set; }

    [Required]
    public string GrandprixType { get; set; }

    [Required]
    public string DriverCount { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public TimeOnly Time { get; set; } 
}