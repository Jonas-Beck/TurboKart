using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TurboKart.Application.Interfaces;
using TurboKart.Domain.Entities;

namespace TurboKart.Presentation.Websites.TurboKartDK
{
    public class BookingModel : PageModel
    {
        private readonly IBookingUseCase bookingUseCase;

        public BookingModel(IBookingUseCase bookingUseCase)
        {
            this.bookingUseCase = bookingUseCase;
        }


        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public double PhoneNumber { get; set; }

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
            //TEMP DATE VALUE
            DateTime tempDate = DateTime.Now;

            Booking booking = new Booking() { BookingId = 0, CustomerId = 0, Start = tempDate };
            bookingUseCase.BookNew(booking);

            return Redirect("/Index");

        }
    }
}
