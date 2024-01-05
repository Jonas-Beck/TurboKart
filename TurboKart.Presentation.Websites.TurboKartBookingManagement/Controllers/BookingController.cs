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

    
    // GET
    public IActionResult Index(BookingsModel? bookingsModel)
    {
        bookingsModel ??= new BookingsModel();

        switch (bookingsModel.TimeFrame)
        {
            // Show Bookings for next week
            case BookingsModel.BookingTimeFrame.Week:
                bookingsModel.Bookings = DummyData();
                bookingsModel.Date = null;
                break;
            // Show Bookings for specific date
            case BookingsModel.BookingTimeFrame.Specific:
                bookingsModel.Bookings = DummyData();
                break;
            // Show Bookings for today
            default:
                bookingsModel.Bookings = DummyData();
                bookingsModel.Date = null;
                break;
        }
        
        return View(bookingsModel);
    }

    // TEMP METHOD FOR DUMMYDATA
    private List<Booking> DummyData()
    {
        var bookings = new List<Booking>()
        {
            new Booking()
            {
                Customer = null,
                BookingId = 1,
                Start = DateTime.Now
            }
        };

        return bookings;
    }


    
    public IActionResult NewBooking()
    {
        return View();
    }
    
    [HttpPost]
    [ActionName("NewBooking")]
    public IActionResult NewBooking(NewBookingModel newBookingModel)
    {
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

            _bookingUseCase.BookNew(booking);

            // Redirect after Creating new booking
            return RedirectToAction("Index", "Booking");
        }

        return View();
    }
}