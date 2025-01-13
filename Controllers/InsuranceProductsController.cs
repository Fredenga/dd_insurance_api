
using InsuranceAPI.DTOs;
using InsuranceAPI.Entities;
using InsuranceAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceProductsController(InsuranceProductRepository repository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsuranceProduct>>> GetAllInsuranceProductsAsync()
        {
            var products = await repository.GetAllInsuranceProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<InsuranceProduct>> GetInsuranceProductAsyncById(int id)
        {
            var product = await repository.GetInsuranceProductAsyncById(id);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<InsuranceProduct>> CreateInsuranceProductAsync([FromBody] InsuranceProductDTO productDTO)
        {
            if (productDTO is null)
            {
                return BadRequest();
            }
            var product = await repository.CreateInsuranceProductAsync(productDTO);
            if (product is null)
            {
                return StatusCode(500, "Insurance Product could not be created");
            }
            return Ok(product);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<InsuranceProduct>> UpdateInsuranceProductAsync(int id, [FromBody] InsuranceProductDTO productDTO)
        {
            if (productDTO is null)
            {
                return BadRequest();
            }
            var product = await repository.UpdateInsuranceProductAsync(id, productDTO);
            if (product is null)
            {
                return StatusCode(500, "Insurance Product could not be updated");
            }
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> DeleteInsuranceProductAsync(int id)
        {

            var message = await repository.DeleteInsuranceProductAsync(id);
            return Ok(message);
        }
    }
}
