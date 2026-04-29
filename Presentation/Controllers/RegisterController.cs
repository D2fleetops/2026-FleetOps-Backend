using fleetops_backend.Application.DTOs;
using fleetops_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace fleetops_backend.Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class RegisterController : ControllerBase
    {
        private readonly AuthService _authService;

        public RegisterController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var success = await _authService.RegisterAsync(dto.FullName, dto.Email, dto.Password);

            if (!success)
                return BadRequest(new { message = "Full Name already exists" });

            return Ok(new { message = "User registered successfully" });
        }
    }
}
