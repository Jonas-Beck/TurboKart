using Microsoft.AspNetCore.Mvc;
using TurboKart.Presentation.Apis.TurboKartAPI.Models;

namespace TurboKart.Presentation.Apis.TurboKartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingUseCase bookingUseCase;

        public BookingController(IBookingUseCase bookingUseCase)
        {
            this.bookingUseCase = bookingUseCase;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Booking>> GetAllBookings()
        {
            return Ok(bookingUseCase.GetAllBookings());
        }

        [HttpGet("{id}")]
        public ActionResult<Booking> GetSingleBooking(int id)
        {
            var result = bookingUseCase.GetSingleBooking(id);
            if (result == null)
                return NotFound("No booking found with that ID");

            return Ok(result);
        }

        [HttpGet("today")]
        public ActionResult<IEnumerable<Booking>> GetTodaysBooking()
        {
            var result = bookingUseCase.GetTodaysBookings();
            if (result.Count() == 0)
                return NotFound("No bookings today");

            return Ok(bookingUseCase.GetTodaysBookings());
        }

        [HttpPost]
        public ActionResult NewBook(BookingRequest request)
        {
            Booking booking = request.booking;
            Customer customer = request.customer;

            bookingUseCase.BookNew(booking, customer);

            return Ok();
        }
    }
}
