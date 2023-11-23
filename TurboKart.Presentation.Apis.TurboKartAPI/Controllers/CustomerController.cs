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

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAllCustomers()
        {
            return Ok(customerUseCase.GetAllCustomers());
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetSingleCustomer(int id)
        {
            var result = customerUseCase.GetSingleCustomer(id);
            if (result == null)
                return NotFound("No customer found with that ID");

            return Ok(result);
        }

    }
}
