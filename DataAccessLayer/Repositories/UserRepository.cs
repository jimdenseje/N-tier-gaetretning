using Models;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User? GetUserByUserId(int userid)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userid);
        }
    }
}
