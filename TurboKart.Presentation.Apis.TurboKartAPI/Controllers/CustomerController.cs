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
            return Ok( await customerUseCase.GetAllCustomers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetSingleCustomer(int id)
        {
            var result = await customerUseCase.GetSingleCustomer(id);
            if (result == null)
                return NotFound("No customer found with that ID");

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(Customer customer)
        {
            try
            {
                await customerUseCase.Update(customer);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();

            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(Customer customer)
        {
            try
            {
                await customerUseCase.Delete(customer);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();

            }
        }

    }
}
