using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgeGroupController : ControllerBase
    {
        private readonly IAgeGroupService _ageGroupService;

        public AgeGroupController(IAgeGroupService ageGroupService)
        {
            _ageGroupService = ageGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAgeGroups()
        {
            var products = await _ageGroupService.GetAllAgeGroupsAsync();
            return Ok(products);
        }
    }
}
