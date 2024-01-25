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
    public async Task<IActionResult> NewBooking(BookingModel bookingModel)
    {
        // Validate that newBookingModel ModelState is valid
        if (ModelState.IsValid)
        {
            // Create new customer Object to add to Database
            Customer customer = new Customer
            {
                Name = bookingModel.Name,
                Email = bookingModel.Email,
                Phonenumber = bookingModel.PhoneNumber,
                CustomerId = 0,
                Bookings = null
            };

            // Create new Booking Object to add to Database
            Booking booking = new Booking
            {
                // Convert DateOnly and TimeOnly to DateTime
                Start = bookingModel.Date.ToDateTime(bookingModel.Time),
                DriverCount = bookingModel.DriverCount,
                Type = bookingModel.Type,
                Customer = customer,
                CustomerId = 0,
            };

            // Call API to add new booking using bookingUseCase.BookNew()
            await _bookingUseCase.BookNew(booking);

            // Redirect after Creating new booking
            return RedirectToAction("Index", "Booking", null);
        }

        // Return newBooking view if ModelState is invalid 
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteBooking(int bookingId, BookingsModel bookingsModel)
    {
        await _bookingUseCase.Delete(bookingId);

        return RedirectToAction("Index", "Booking", bookingsModel);
    }

    [HttpGet]
    [ActionName("EditBooking")]
    public  IActionResult EditBooking(int id)
    {
        // Get Booking object from API
        Booking booking = _bookingUseCase.GetSingleBooking(id).Result;
        
        // Create BookingModel to display data
        BookingModel test = new()
        {
            CustomerId = booking.CustomerId,
            BookingId = booking.BookingId,
            Name = booking.Customer.Name,
            Email = booking.Customer.Email,
            PhoneNumber = booking.Customer.Phonenumber,
            Date = DateOnly.FromDateTime(booking.Start),
            DriverCount = booking.DriverCount,
            Type = booking.Type,
            Time = TimeOnly.FromDateTime(booking.Start)
        };
        
        // Return View with BookingModel
        return View(test);
    } 
    
    
    [HttpPost]
    [ActionName("EditBooking")]
    public async Task<IActionResult> EditBooking(BookingModel bookingModel)
    {
        if (ModelState.IsValid)
        {
            // Create the new booking object with data from bookingModel
            Booking booking = new()
            {
                CustomerId = bookingModel.CustomerId,
                BookingId = bookingModel.BookingId,
                Type = bookingModel.Type,
                DriverCount = bookingModel.DriverCount,
                Start = bookingModel.Date.ToDateTime(bookingModel.Time)
            };

            // Update booking using bookingUseCase to call API
            await _bookingUseCase.Update(booking);

            // Redirect to index again without bookingsModel
            return RedirectToAction("Index", "Booking", null);
        }

        return View(bookingModel);
    } 
    
}