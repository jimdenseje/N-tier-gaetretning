using Models;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        User? GetUserByUsername(string username);
        User? GetUserByUserId(int userid);
        Task AddAsync(User user);
    }
}
