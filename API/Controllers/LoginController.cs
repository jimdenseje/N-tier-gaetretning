using BusinessLogicLayer.Interfaces;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            var token = _authService.Login(request);
            if (token == null)
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(new { token });
        }
    }
}
