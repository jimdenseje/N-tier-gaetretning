using Models;

namespace DataAccessLayer.Interfaces
{
    public interface IScoreRepository
    {
        Task<IEnumerable<Score>> GetAllAsync();
        Task<IEnumerable<Score>> GetScoresByDateRange(DateTime start, DateTime end);
        Task <Score> AddAsync(Score item);
        bool GetScoreByUserAndChallengeAsync(User user, DailyChallenge dailyChallenge);
    }
}
