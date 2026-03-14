using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IAgeGroupService _ageGroupService;

        public AuthService(
            IUserRepository userRepository,
            IConfiguration configuration,
            IAgeGroupService ageGroupService
        ) {
            _userRepository = userRepository;
            _ageGroupService = ageGroupService;
            _configuration = configuration;
        }

        public string? Login(LoginRequestDto request)
        {
            var user = _userRepository.GetUserByUsername(request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiryMinutes"])),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public UserDto SignUp(CreateUserRequestDto createUserRequestDto)
        {
            if (createUserRequestDto.Username == "")
                throw new ArgumentException("Username cannot be empty.");

            if (createUserRequestDto.Password == "")
                throw new ArgumentException("Password cannot be empty.");

            if (_userRepository.GetUserByUsername(createUserRequestDto.Username) != null)
                throw new ArgumentException("Username already exists.");

            AgeGroup ageGroup = _ageGroupService.GetAgeGroupIdByAge(createUserRequestDto.Age);

            var user = new User
            {
                Username = createUserRequestDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(createUserRequestDto.Password),
                AgeGroup = ageGroup
            };

            _userRepository.AddAsync(user).Wait();

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                AgeGroupId = user.AgeGroupId
            };
        }
    }
}
