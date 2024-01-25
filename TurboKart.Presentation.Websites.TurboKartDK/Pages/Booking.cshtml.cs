using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TurboKart.Application.Interfaces;
using TurboKart.Domain.Entities;

namespace TurboKart.Presentation.Websites.TurboKartDK.Pages
{
    public class BookingModel : PageModel
    {
        private readonly IBookingUseCase _bookingUseCase;

        public BookingModel(IBookingUseCase bookingUseCase)
        {
            this._bookingUseCase = bookingUseCase;
        }


        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string PhoneNumber { get; set; }

        [BindProperty]
        public string GrandprixType { get; set; }

        [BindProperty]
        public string DriverCount { get; set; }

        [BindProperty]
        public DateOnly Date { get; set; }

        [BindProperty]
        public TimeOnly Time { get; set; }



        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            
            // Create new customer Object to add to Database
            Customer customer = new Customer
            {
                Name = Name,
                Email = Email,
                Phonenumber = PhoneNumber,
                CustomerId = 0,
                Bookings = null
            };

            // Create new Booking Object to add to Database
            Booking booking = new Booking
            {
                // Convert DateOnly and TimeOnly to DateTime
                Start = Date.ToDateTime(Time),
                Customer = customer,
                CustomerId = 0,
            };

            _bookingUseCase.BookNew(booking);

            return Redirect("/Index");

        }
    }
}
