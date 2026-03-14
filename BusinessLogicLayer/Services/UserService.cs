using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DTOs;
using Models;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAgeGroupService _ageGroupService;

        public UserService(
            IUserRepository userRepository,
            IAgeGroupService ageGroupService
        ) {
            _userRepository = userRepository;
            _ageGroupService = ageGroupService;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var scores = await _userRepository.GetAllAsync();
            return scores.Select(s => new UserDto
            {
                Id = s.Id,
                Username = s.Username,
                AgeGroupId = s.AgeGroupId
            }).ToList();
        }

        public UserDto? GetByUsername(string username)
        {
            User? user = _userRepository.GetUserByUsername(username);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                AgeGroupId = user.AgeGroupId
            };
        }
    }
}
