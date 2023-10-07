using eTech.Auth;
using eTech.Entities;
using eTech.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eTech.Services {
    public class TokenService : ITokenService {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _signingKey;
        public TokenService(IConfiguration configuration) {
            _configuration = configuration;
            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        }

        public string GenerateAccessToken(ApplicationUser user) {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(7),
                claims: claims,
                signingCredentials: new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(ApplicationUser user) {
            var token = new RefreshToken {
                Token = Guid.NewGuid().ToString() + user.Id,
                ExpireAt = DateTime.Now.AddDays(7)
            };
            return token;
        }

        public List<Claim> GetClaimsFromExpiredToken(string token) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            return jwtSecurityToken.Claims.ToList();
        }

        public bool ValidateRefreshToken(string token) {
            throw new NotImplementedException();
        }
    }
}
