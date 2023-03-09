using Realta.Contract.AuthenticationWebAPI;

namespace Realta.WebAPI.Authentication
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();

        string GenerateRefreshToken();
    }
}
