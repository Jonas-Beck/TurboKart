using Microsoft.AspNetCore.Mvc;

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
            if (!result.Result.Any())
                return NotFound("No bookings today");

            return Ok(bookingUseCase.GetTodaysBookings());
        }

        [HttpPost("new")]
        public ActionResult NewBook(Booking booking)
        {
            try
            {
                bookingUseCase.BookNew(booking);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public ActionResult Update(Booking booking)
        {
            try
            {
                bookingUseCase.Update(booking);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();

            }
        }

        [HttpDelete("delete")]
        public ActionResult Delete(Booking booking)
        {
            try
            {
                bookingUseCase.Delete(booking);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();

            }
        }
    }
}
