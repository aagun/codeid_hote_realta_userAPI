using Microsoft.IdentityModel.Tokens;
using Realta.Contract.AuthenticationWebAPI;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Realta.WebAPI.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IConfiguration _configuration;
        private Users userCred;
        private Roles userRole;
        private readonly IRepositoryManager _repositoryManager;

        public AuthenticationManager(IConfiguration configuration, IRepositoryManager repositoryManager)
        {
            _configuration = configuration;
            _repositoryManager = repositoryManager;
        }

        public async Task<string> CreateToken()
        {
            var signInCredential = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signInCredential, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return (Convert.ToBase64String(randomNumber));
            }
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            userCred = _repositoryManager.UsersRepository.FindUserByEmail(userForAuth.UserEmail);
            bool isSuccess = _repositoryManager.UsersRepository.SignIn(userForAuth.UserEmail, userForAuth.Password);
            if (userCred != null && isSuccess)
            {
                userRole = _repositoryManager.UsersRepository.GetRoles(userForAuth.UserEmail);
                return true;
            }
            return false;
        }

        private SigningCredentials GetSigningCredentials()
        {
        
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("SecretKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private Task<List<Claim>> GetClaims()
        {
            return Task.FromResult(new List<Claim>() {
                new Claim(ClaimTypes.Email, userCred.UserEmail),
                new Claim(ClaimTypes.Role, userRole.RoleName)
            });
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}
