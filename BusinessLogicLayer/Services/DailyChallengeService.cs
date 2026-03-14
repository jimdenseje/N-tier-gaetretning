using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Models;

namespace BusinessLogicLayer.Services
{
    public class DailyChallengeService : IDailyChallengeService
    {
        private readonly IDailyChallengeRepository _dailyChallengeRepository;

        public DailyChallengeService(IDailyChallengeRepository dailyChallengeRepository)
        {
            _dailyChallengeRepository = dailyChallengeRepository;
        }

        public async Task<IEnumerable<DailyChallenge>> GetAllDailyChallengesAsync()
        {
            return await _dailyChallengeRepository.GetAllAsync();
        }

        private short GenerateDirection()
        {
            var random = new Random();
            return (short)random.Next(0, 360);
        }

        public async Task<DailyChallenge> GetDailyChallengeAsync()
        {
            var today = DateTime.UtcNow.Date;

            var challenge = await _dailyChallengeRepository
                .FirstOrDefaultAsync(c => c.ChallengeDate == today);

            if (challenge != null)
                return challenge;

            var newChallenge = new DailyChallenge
            {
                ChallengeDate = today,
                Direction = GenerateDirection()
            };

            await _dailyChallengeRepository.AddAsync(newChallenge);

            return newChallenge;
        }

    }
}
