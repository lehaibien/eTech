using eTech.Auth;
using eTech.Entities;
using System.Security.Claims;

namespace eTech.Services.Interfaces {
    public interface ITokenService {
        string GenerateAccessToken(ApplicationUser user);
        RefreshToken GenerateRefreshToken(ApplicationUser user);
        List<Claim> GetClaimsFromExpiredToken(string token);
        bool ValidateRefreshToken(string token);
    }
}
