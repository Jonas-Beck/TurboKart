using Microsoft.AspNetCore.Mvc;

namespace TurboKart.Presentation.Apis.TurboKartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerUseCase customerUseCase;

        public CustomerController(ICustomerUseCase customerUseCase)
        {
            this.customerUseCase = customerUseCase;
        }

        [HttpGet("all")]
        public  async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            // Call the GetALlCustomers method from the CustomerUseCase to retrieve all customers
            // and wrap the result in an OkObjectResult to signify a successful HTTP response.
            return Ok( await customerUseCase.GetAllCustomers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetSingleCustomer(int id)
        {
            // Call the GetSingleCustomer method from the customerUseCase to retrieve customer with specific id
            var result = await customerUseCase.GetSingleCustomer(id);
            
            if (result == null)
                // Return NotFound to signify a unsuccessful HTTP response
                return NotFound("No customer found with that ID");

            // wrap the result in an OkObjectResult to signify a successful HTTP response.
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(Customer customer)
        {
            try
            {
                // Call the Update method from the CustomerUseCase to update an existing customer
                await customerUseCase.Update(customer);
                
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
        public async Task<ActionResult> Delete(Customer customer)
        {
            try
            {
                // Call the Delete method from the CustomerUseCase to delete an existing customer
                await customerUseCase.Delete(customer);
                
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
