using Microsoft.AspNetCore.Mvc;
using TurboKart.Application.Interfaces;
using TurboKart.Domain.Entities;
using TurboKart.Presentation.Websites.TurboKartBookingManagement.Models;

namespace TurboKart.Presentation.Websites.TurboKartBookingManagement.Controllers;

public class BookingController : Controller
{
    
    private readonly IBookingUseCase _bookingUseCase;

    public BookingController(IBookingUseCase bookingUseCase)
    {
        _bookingUseCase = bookingUseCase;
    }

    
    [HttpGet]
    public IActionResult Index(BookingsModel? bookingsModel)
    {
        // Check if bookingsModel is null. If true initialize new BookingsModel
        bookingsModel ??= new BookingsModel();

        // Switch statement to get bookings from API based on bookingsModel.TimeFrame
        switch (bookingsModel.TimeFrame)
        {
            // Show Bookings for next week
            case BookingsModel.BookingTimeFrame.Week:
                bookingsModel.Bookings = _bookingUseCase.GetWeeksBookings().Result.ToList();
                bookingsModel.Date = null;
                break;
            // Show Bookings for specific date
            case BookingsModel.BookingTimeFrame.Specific:
                bookingsModel.Bookings = _bookingUseCase.GetSpecificDateBookings(bookingsModel.Date.Value).Result.ToList();
                break;
            // Show Bookings for today
            default:
                bookingsModel.Bookings = _bookingUseCase.GetTodaysBookings().Result.ToList();
                bookingsModel.Date = null;
                break;
        }
        
        // Return View with bookingsModel populated with bookings
        return View(bookingsModel);
    }
    
    
    // Controller to handle creating a bookingsModel for specific date and redirection to Index action
    [HttpGet]
    public IActionResult IndexSpecific(DateOnly date)
    {
        // Create new bookingsModel
        var bookingsModel = new BookingsModel();

        // Set values of Date and TimeFrame
        bookingsModel.Date = date;
        bookingsModel.TimeFrame = BookingsModel.BookingTimeFrame.Specific;

        // Redirect to Index action to populate model with data
        return RedirectToAction("Index", bookingsModel);
    }

    [HttpGet]
    public IActionResult NewBooking()
    {
        return View();
    }
    
    [HttpPost]
    [ActionName("NewBooking")]
    public IActionResult NewBooking(NewBookingModel newBookingModel)
    {
        // Validate that newBookingModel ModelState is valid
        if (ModelState.IsValid)
        {
            // Create new customer Object to add to Database
            Customer customer = new Customer
            {
                Name = newBookingModel.Name,
                CustomerId = 0,
                Bookings = null
            };

            // Create new Booking Object to add to Database
            Booking booking = new Booking
            {
                // Convert DateOnly and TimeOnly to DateTime
                Start = newBookingModel.Date.ToDateTime(newBookingModel.Time),
                Customer = customer,
                CustomerId = 0,
            };

            // Call API to add new booking using bookingUseCase.BookNew()
            _bookingUseCase.BookNew(booking);

            // Redirect after Creating new booking
            return RedirectToAction("Index", "Booking");
        }

        // Return newBooking view if ModelState is invalid 
        return View();
    }

    [HttpPost]
    public IActionResult DeleteBooking(int bookingId, BookingsModel bookingsModel)
    {
        _bookingUseCase.Delete(bookingId);

        return RedirectToAction("Index", bookingsModel);
    }
    
}