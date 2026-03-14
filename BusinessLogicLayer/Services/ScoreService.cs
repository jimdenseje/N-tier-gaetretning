using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DTOs;
using Models;

namespace BusinessLogicLayer.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDailyChallengeRepository _dailyChallengeRepository;

        public ScoreService(
            IScoreRepository scoreRepository,
            IUserRepository userRepository,
            IDailyChallengeRepository dailyChallengeRepository)
        {
            _scoreRepository = scoreRepository;
            _userRepository = userRepository;
            _dailyChallengeRepository = dailyChallengeRepository;
        }

        // Get all scores with user and challenge details
        public async Task<IEnumerable<ScoreDto>> GetAllScoresAsync()
        {
            var scores = await _scoreRepository.GetAllAsync();

            return scores.Select(s => new ScoreDto
            {
                Id = s.Id,
                ScoreValue = s.ScoreValue,
                UserId = s.UserId,
                Username = s.User.Username,
                AgeGroupId = s.User.AgeGroupId,
                DailyChallengeId = s.DailyChallengeId,
                ChallengeDate = s.DailyChallenge.ChallengeDate
            }).ToList();
        }

        // Get the leaderboard for today and yesterday
        public async Task<ScoreLeaderboardDto> GetLeaderboardAsync()
        {
            // Get today's date and yesterday's date
            var today = DateTime.UtcNow.Date;
            var yesterday = today.AddDays(-1);

            // Get scores for today and yesterday
            var todayScores = await _scoreRepository.GetScoresByDateRange(today, today.AddDays(1));
            var yesterdayScores = await _scoreRepository.GetScoresByDateRange(yesterday, today);

            // Map the scores to DTOs and return the leaderboard
            return new ScoreLeaderboardDto
            {
                Today = todayScores
                    .OrderByDescending(s => s.ScoreValue)
                    .Take(10)
                    .Select(s => new ScoreDto
                    {
                        Id = s.Id,
                        ScoreValue = s.ScoreValue,
                        UserId = s.UserId,
                        Username = s.User.Username,
                        AgeGroupId = s.User.AgeGroupId,
                        DailyChallengeId = s.DailyChallengeId,
                        ChallengeDate = s.DailyChallenge.ChallengeDate
                    })
                    .ToList(),

                Yesterday = yesterdayScores
                    .OrderByDescending(s => s.ScoreValue)
                    .Take(10)
                    .Select(s => new ScoreDto
                    {
                        Id = s.Id,
                        ScoreValue = s.ScoreValue,
                        UserId = s.UserId,
                        Username = s.User.Username,
                        AgeGroupId = s.User.AgeGroupId,
                        DailyChallengeId = s.DailyChallengeId,
                        ChallengeDate = s.DailyChallenge.ChallengeDate
                    })
                    .ToList()
            };
        }

        // Add a new score for the current daily challenge
        public async Task<ScoreDto> AddScoreAsync(AddScoreDto dto, int userId)
        {
            // Validate the score value
            if (dto.ScoreValue <= 0)
                throw new ArgumentException("Score must be positive");

            // Get today's date
            var today = DateTime.UtcNow.Date;

            // Get today's challenge
            var challenge = await _dailyChallengeRepository
                .FirstOrDefaultAsync(c => c.ChallengeDate == today);

            // If no challenge found for today, throw an exception
            if (challenge == null)
                throw new InvalidOperationException("No daily challenge found for today");

            // Get the user
            var user = _userRepository.GetUserByUserId(userId);

            // If user not found, throw an exception
            if (user == null)
                throw new InvalidOperationException("User not found");

            if (_scoreRepository.GetScoreByUserAndChallengeAsync(user, challenge))
                throw new InvalidOperationException("User has already submitted a score for today's challenge");

            // Create the score
            var score = new Score
            {
                ScoreValue = dto.ScoreValue,
                DailyChallenge = challenge,
                User = user,
            };

            // Add the score to the database
            var addedScore = await _scoreRepository.AddAsync(score);

            // Return the added score as a DTO
            return new ScoreDto
            {
                Id = addedScore.Id,
                ScoreValue = addedScore.ScoreValue,
                UserId = addedScore.User.Id,
                Username = addedScore.User.Username,
                AgeGroupId = addedScore.User.AgeGroupId,
                DailyChallengeId = addedScore.DailyChallengeId,
                ChallengeDate = addedScore.DailyChallenge.ChallengeDate
            };
        }
    }
}
