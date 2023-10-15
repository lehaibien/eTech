using eTech.Context;
using eTech.Entities;
using eTech.Entities.Response;
using eTech.Services;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace eTech.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller {
        private readonly IImageService _imageService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserService _userService;
        private readonly ITokenService _tokenService;
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService, ICartService cartService, UserService userService, IImageService imageService) {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _userService = userService;
            _imageService = imageService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserByAccessToken() {
            string accessToken = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            List<Claim> claims = _tokenService.GetClaimsFromExpiredToken(accessToken);    
            string userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.Users.Include(u => u.Image).Include(u => u.Addresses).SingleAsync(u => u.Id == userId); 
            string role = string.Join(",", await _userManager.GetRolesAsync(user)) == "Admin,User" ? "Admin":"User";
            if (user == null) {
                return NotFound();
            }
            var u = new UserResponse {
                Id = user.Id,
                Username = user.UserName,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Image = user.Image,
                Address = user.Address ?? new Address(),
                Orders = user.Orders ?? new List<Order>(),
            };
            return Ok(u);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateImageUser([FromForm] string Id, IFormFile File)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            Image img = _imageService.Upload(File).Result;
            user.Image = img;
            //_context.Images.Add(img);
            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();
            var u = new UserResponse
            {
                Id = user.Id,
                Username = user.UserName,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Image = user.Image,
                Role = string.Join(",", await _userManager.GetRolesAsync(user)) == "Admin,User" ? "Admin" : "User"
            };
            return Ok(u);
        }
    }
}
