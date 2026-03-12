using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyChallengeController : ControllerBase
    {
        private readonly IDailyChallengeService _dailyChallengeService;

        public DailyChallengeController(IDailyChallengeService dailyChallengeService)
        {
            _dailyChallengeService = dailyChallengeService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetDailyChallenges()
        {
            var item = await _dailyChallengeService.GetAllDailyChallengesAsync();
            return Ok(item);
        }

        [HttpGet("today")]
        public async Task<ActionResult<DailyChallenge>> GetTodayChallenge()
        {
            var challenge = await _dailyChallengeService.GetDailyChallengeAsync();
            return Ok(challenge);
        }
    }
}
