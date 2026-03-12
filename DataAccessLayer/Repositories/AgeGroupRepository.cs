using Models;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class AgeGroupRepository : IAgeGroupRepository
    {
        private readonly AppDbContext _context;

        public AgeGroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AgeGroup>> GetAllAsync()
        {
            return await _context.AgeGroups.ToListAsync();
        }
    }
}
