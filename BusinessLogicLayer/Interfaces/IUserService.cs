using DTOs;
using Models;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        UserDto? GetByUsername(string username);
    }
}
