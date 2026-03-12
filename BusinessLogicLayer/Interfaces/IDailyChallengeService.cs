using Models;

namespace BusinessLogicLayer.Interfaces
{
    public interface IDailyChallengeService
    {
        Task<IEnumerable<DailyChallenge>> GetAllDailyChallengesAsync();
        Task<DailyChallenge> GetDailyChallengeAsync();
    }
}
