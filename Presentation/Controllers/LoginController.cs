using fleetops_backend.Application.DTOs;
using fleetops_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace fleetops_backend.Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class LoginController : ControllerBase
    {
        private readonly AuthService _authService;

        public LoginController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var token = await _authService.LoginAsync(dto.Email, dto.Password);

            if (token == null)
                return Unauthorized(new { message = "Invalid credentials" });

            return Ok(new { token });
        }
    }
}