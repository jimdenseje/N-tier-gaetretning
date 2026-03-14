using Models;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Interfaces;
using System.ComponentModel;

namespace DataAccessLayer.Repositories
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly AppDbContext _context;

        public ScoreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Score>> GetAllAsync()
        {
            return await _context.Scores
                .Include(s => s.User)
                .Include(s => s.DailyChallenge)
                .ToListAsync();
        }

        public async Task<IEnumerable<Score>> GetScoresByDateRange(DateTime start, DateTime end)
        {
            return await _context.Scores
                .Include(s => s.User)
                .Include(s => s.DailyChallenge)
                .Where(s => s.DailyChallenge.ChallengeDate >= start && s.DailyChallenge.ChallengeDate < end)
                .ToListAsync();
        }

        public async Task<Score> AddAsync(Score item)
        {
            _context.Scores.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public bool GetScoreByUserAndChallengeAsync(User user, DailyChallenge dailyChallenge)
        {
            return _context.Scores.AnyAsync(s => s.UserId == user.Id && s.DailyChallengeId == dailyChallenge.Id).Result;
        }
    }
}
