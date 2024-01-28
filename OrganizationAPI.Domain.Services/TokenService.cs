using Microsoft.IdentityModel.Tokens;
using OrganizationAPI.API.Infrastructure.Settings;
using OrganizationAPI.Domain.Abstractions.DTOs.Account;
using OrganizationAPI.Domain.Abstractions.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrganizationAPI.Domain.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings jwtSettings;
        public TokenService(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        public string GenerateJwtToken(AccountDto account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: GetClaims(account),
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpirationInMinutes),
                signingCredentials: credentials);

            return tokenHandler.WriteToken(token);
        }

        private IEnumerable<Claim> GetClaims(AccountDto dto)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, dto.Id.ToString()),
                new Claim(ClaimTypes.Role, dto.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, dto.Email),
                new Claim(JwtRegisteredClaimNames.NameId, dto.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
        }
    }
}
