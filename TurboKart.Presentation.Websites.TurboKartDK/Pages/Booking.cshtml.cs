using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TurboKart.Presentation.Websites.TurboKartDK
{
    public class BookingModel : PageModel
    {
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

        public void OnPost()
        {

        }

    }
}
