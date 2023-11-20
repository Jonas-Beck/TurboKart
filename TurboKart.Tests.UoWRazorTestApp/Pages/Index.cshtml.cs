using Microsoft.AspNetCore.Mvc.RazorPages;

using TurboKart.Application.Interfaces;

namespace TurboKart.Tests.UoWRazorTestApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBookingUseCase bookingUseCase;

        public IndexModel(ILogger<IndexModel> logger, IBookingUseCase bookingUseCase)
        {
            _logger = logger;
            this.bookingUseCase = bookingUseCase;
        }

        public IBookingUseCase BookingUseCase => bookingUseCase;

        public void OnGet()
        {

        }
    }
}