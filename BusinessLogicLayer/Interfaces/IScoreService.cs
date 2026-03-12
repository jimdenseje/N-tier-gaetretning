using DTOs;

namespace BusinessLogicLayer.Interfaces
{
    public interface IScoreService
    {
        Task<IEnumerable<ScoreDto>> GetAllScoresAsync();
        Task<ScoreLeaderboardDto> GetLeaderboardAsync();
        Task<ScoreDto> AddScoreAsync(AddScoreDto dto, int userId);
    }
}
