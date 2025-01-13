using InsuranceAPI.DTOs;
using InsuranceAPI.Entities;
using InsuranceAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(AdminRepository repository) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<Admin>> Register([FromBody] AdminDTO admin)
        {
            if(admin is null)
            {
                return BadRequest();
            }
            var newAdmin = await repository.Register(admin);
            if (newAdmin is null)
            {
                return StatusCode(500, "Internal Server Error");
            }
            return Ok(newAdmin);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest request)
        {
            if (request.Email is null || request.Password is null)
            {
                return BadRequest();
            }
            var accessToken = await repository.Login(request);
            return Ok(accessToken);
        }
    }
}
