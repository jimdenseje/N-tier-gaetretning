using DTOs;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAuthService
    {
        string? Login(LoginRequestDto request);
    }
}
