using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StepsLeaderboard.WebAPI.Authentication
{
    public class TokenService
    {
        private readonly AuthSettings _authSettings;

        public TokenService(IOptions<AuthSettings> authSettings)
        {
            _authSettings = authSettings.Value;
        }

        public string GenerateToken(string username)
        {
            // In production, more claims could be added, e.g., roles, user ID, or team membership
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                // new Claim("role", "Admin"), etc.
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _authSettings.Issuer,
                audience: _authSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

