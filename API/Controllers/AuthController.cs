using Azure.Core;
using BusinessLogicLayer.Interfaces;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            var token = _authService.Login(request);
            if (token == null)
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(new { token });
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] CreateUserRequestDto createUserRequestDto)
        {
            try
            {
                var user = _authService.SignUp(createUserRequestDto);

                LoginRequestDto loginRequesDto = new ()
                {
                    Username = createUserRequestDto.Username,
                    Password = createUserRequestDto.Password
                };

                var token = _authService.Login(loginRequesDto);
                if (token == null)
                    return Unauthorized(new { message = "Invalid username or password" });

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
