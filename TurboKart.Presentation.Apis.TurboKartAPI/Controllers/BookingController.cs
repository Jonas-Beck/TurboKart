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
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
        {
            return Ok( await bookingUseCase.GetAllBookings());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetSingleBooking(int id)
        {
            var result = await bookingUseCase.GetSingleBooking(id);
            if (result == null)
                return NotFound("No booking found with that ID");

            return Ok(result);
        }

        [HttpGet("today")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetTodaysBooking()
        {
            return Ok(await bookingUseCase.GetTodaysBookings());
        }

        [HttpPost("new")]
        public async Task<ActionResult> NewBook(Booking booking)
        {
            try
            {
                await bookingUseCase.BookNew(booking);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(Booking booking)
        {
            try
            {
                await bookingUseCase.Update(booking);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();

            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(Booking booking)
        {
            try
            {
                await bookingUseCase.Delete(booking);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();

            }
        }
    }
}
