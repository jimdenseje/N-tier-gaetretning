using BusinessLogicLayer.Interfaces;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly IScoreService _scoreService;

        public ScoreController(
            IScoreService scoreService
        ) {
            _scoreService = scoreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetScores()
        {
            var products = await _scoreService.GetAllScoresAsync();
            return Ok(products);
        }

        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboard()
        {
            var result = await _scoreService.GetLeaderboardAsync();
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddScore([FromBody] AddScoreDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (userId == null) throw new Exception("Unable to find User Id");

            try
            {
                var score = await _scoreService.AddScoreAsync(dto, int.Parse(userId));
                return CreatedAtAction(nameof(GetScores), new { id = score.Id }, score);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
