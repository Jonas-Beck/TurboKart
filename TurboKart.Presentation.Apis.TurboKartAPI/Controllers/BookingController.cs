using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using TurboKart.Domain.Exceptions;

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
        
        [HttpGet("week")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetWeeksBookings()
        {
            // Call the GetWeeksBookings method from the bookingUseCase to retrieve all bookings for the next 7 days
            // and wrap the result in an OkObjectResult to signify a successful HTTP response.
            return Ok(await bookingUseCase.GetWeeksBookings());
        }
        
        [HttpGet("specific/{date}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetSpecificDateBookings(DateOnly date)
        {
            // Call the GetWeeksBookings method from the bookingUseCase to retrieve all bookings for the next 7 days
            // and wrap the result in an OkObjectResult to signify a successful HTTP response.
            return Ok(await bookingUseCase.GetSpecificDateBookings(date));
        }
        
        [HttpPost("new")]
        public async Task<ActionResult> NewBook(Booking booking)
        {
            try
            {
                // Call the GetSpecificDateBookings using bookings start date to get bookings same day
                var overlappingBookings = await bookingUseCase.GetOverlappingBookings(booking.Time);

                // Call the BookNew method from the BookingUseCase to create new booking
                await bookingUseCase.BookNew(booking, overlappingBookings);

                // Return OkObjectResult to signify a successful HTTP response
                return Ok();
            }
            catch (InvalidDriverCountException)
            {
                return BadRequest("Driver count cannot be above 20");
            }
            catch (NotEnoughSpaceException)
            {
                return BadRequest($"Not enough space for {booking.DriverCount} driver(s)");
            }
            catch (Exception e)
            {
                // Return BadRequestResult to signify a unsuccessful HTTP response
                return BadRequest(e.Message);
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

        [HttpDelete("delete/{bookingId}")]
        public async Task<ActionResult> Delete(int bookingId)
        {
            try
            {
                // Call the Delete method from the BookingUseCase to delete an existing booking
                await bookingUseCase.Delete(bookingId);
                
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
