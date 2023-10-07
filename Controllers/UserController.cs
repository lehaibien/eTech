using eTech.Entities;
using eTech.Entities.Response;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eTech.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService) {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserByAccessToken() {
            string accessToken = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            List<Claim> claims = _tokenService.GetClaimsFromExpiredToken(accessToken);    
            string userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user == null) {
                return NotFound();
            }
            var u = new UserResponse {
                Id = user.Id,
                Username = user.UserName,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Image = user.Image
            };
            return Ok(u);
        }
    }
}
