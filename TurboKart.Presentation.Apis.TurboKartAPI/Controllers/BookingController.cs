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
            // Call the GetAllBookings method from the bookingUseCase to retrieve all bookings
            // and wrap the result in an OkObjectResult to signify a successful HTTP response.
            return Ok( await bookingUseCase.GetAllBookings());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetSingleBooking(int id)
        {
            // Call the GetSingleBooking method from the bookingUseCase to retrieve booking with specific id
            var result = await bookingUseCase.GetSingleBooking(id);
            
            if (result == null)
                // Return NotFound to signify a unsuccessful HTTP response
                return NotFound("No booking found with that ID");

            // wrap the result in an OkObjectResult to signify a successful HTTP response.
            return Ok(result);
        }

        [HttpGet("today")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetTodaysBooking()
        {
            // Call the GetTodaysBookings method from the bookingUseCase to retrieve all bookings today
            // and wrap the result in an OkObjectResult to signify a successful HTTP response.
            return Ok(await bookingUseCase.GetTodaysBookings());
        }

        [HttpPost("new")]
        public async Task<ActionResult> NewBook(Booking booking)
        {
            try
            {
                // Call the BookNew method from the BookingUseCase to create new booking
                await bookingUseCase.BookNew(booking);
                
                // Return OkObjectResult to signify a successful HTTP response
                return Ok();
            }
            catch (Exception e)
            {
                // Return BadRequestResult to signify a unsuccessful HTTP response
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(Booking booking)
        {
            try
            {
                // Call the Update method from the BookingUseCase to update an existing booking
                await bookingUseCase.Update(booking);
                
                // Return OkObjectResult to signify a successful HTTP response
                return Ok();
            }
            catch (Exception e)
            {
                // Return BadRequestResult to signify a unsuccessful HTTP response
                return BadRequest();

            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(Booking booking)
        {
            try
            {
                // Call the Delete method from the BookingUseCase to delete an existing booking
                await bookingUseCase.Delete(booking);
                
                // Return OkObjectResult to signify a successful HTTP response
                return Ok();
            }
            catch (Exception e)
            {
                // Return BadRequestResult to signify a unsuccessful HTTP response
                return BadRequest();
            }
        }
    }
}
