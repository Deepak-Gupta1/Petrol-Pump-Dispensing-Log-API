using FuelDispensingAPI.Application.Interface;
using FuelDispensingAPI.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FuelDispensingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.AuthenticateAsync(request);
            if (token == null)
                return Unauthorized(new ResponseModel<bool>(false, false, "Invalid username or password."));

            return Ok(new { token });
        }

    }
}
