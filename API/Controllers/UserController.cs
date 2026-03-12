using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var products = await _userService.GetAllUsersAsync();
            return Ok(products);
        }
    }
}
