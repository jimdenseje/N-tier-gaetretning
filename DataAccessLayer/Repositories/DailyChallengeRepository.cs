using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories
{
    public class DailyChallengeRepository : IDailyChallengeRepository
    {
        private readonly AppDbContext _context;

        public DailyChallengeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DailyChallenge>> GetAllAsync()
        {
            return await _context.DailyChallenges.ToListAsync();
        }

        public async Task AddAsync(DailyChallenge item)
        {
            await _context.DailyChallenges.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<DailyChallenge?> FirstOrDefaultAsync(Expression<Func<DailyChallenge, bool>> predicate)
        {
            return await _context.DailyChallenges.FirstOrDefaultAsync(predicate);
        }

    }
}
