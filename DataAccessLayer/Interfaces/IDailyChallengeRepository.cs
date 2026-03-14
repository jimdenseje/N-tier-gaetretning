using Models;
using System.Linq.Expressions;

namespace DataAccessLayer.Interfaces
{
    public interface IDailyChallengeRepository
    {
        Task<IEnumerable<DailyChallenge>> GetAllAsync();
        Task AddAsync(DailyChallenge item);
        Task<DailyChallenge?> FirstOrDefaultAsync(Expression<Func<DailyChallenge, bool>> predicate);
    }
}
