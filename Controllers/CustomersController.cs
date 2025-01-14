using InsuranceAPI.Entities;
using InsuranceAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(ICustomer repository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllAsync()
        {
            var customers = await repository.GetAllAsync();
            return Ok(customers);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetByIdAsync(int id)
        {
            var customer = await repository.GetByIdAsync(id);
            if(customer is null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateAsync([FromBody] Customer customer)
        {
            if (customer is null) {
                return BadRequest();
            }
            var newCustomer = await repository.CreateAsync(customer);
            return Ok(newCustomer);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Customer>> UpdateAsync(int id, [FromBody] Customer customer)
        {
            if (customer is null)
            {
                return BadRequest();
            }
            var newCustomer = await repository.UpdateAsync(id, customer);
            return Ok(newCustomer);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> UpdateAsync(int id)
        {
            var message = await repository.DeleteAsync(id);
            if(message.Length == 0)
            {
                return NotFound("Specific customer not found");
            }
            return Ok(message);
        }
    }
}
