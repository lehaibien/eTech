using eTech.Entities;
using eTech.Entities.Requests;
using eTech.Entities.Response;
using eTech.Services;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Xml.Linq;

namespace eTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IImageService _imageService;
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;

        public UserController(UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              ITokenService tokenService,
                              IImageService imageService,
                              IUserService userService,
                              IAddressService addressService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _imageService = imageService;
            _userService = userService;
            _addressService = addressService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserByAccessToken()
        {
            string accessToken = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            List<Claim> claims = _tokenService.GetClaimsFromExpiredToken(accessToken);
            string userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.Users.Include(u => u.Image).Include(u => u.Addresses).SingleAsync(u => u.Id == userId);
            string role = string.Join(",", await _userManager.GetRolesAsync(user)) == "Admin,User" ? "Admin" : "User";
            if (user == null)
            {
                return NotFound();
            }
            var u = new UserResponse
            {
                Id = user.Id,
                Username = user.UserName,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Addresses,
                Image = user.Image,
                Role = role
            };
            return Ok(u);
        }

        [HttpPost("Image")]
        [Authorize]
        public async Task<IActionResult> UpdateImageUser([FromForm] string Id, IFormFile File)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            if (user.Image != null)
            {
                _imageService.DeleteImage(user.Image);
            }
            user.Image = _userService.UpdateImageUser(File).Result;
            await _userManager.UpdateAsync(user);
            return Ok();
        }

        [HttpPost("Name")]
        [Authorize]
        public async Task<IActionResult> UpdateNameUser([FromForm] string Id, string Name)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            user.Name = Name;
            await _userManager.UpdateAsync(user);
            return Ok();
        }

        [HttpPost("Email")]
        [Authorize]
        public async Task<IActionResult> UpdateEmail([FromForm] string Id, string Email)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            user.Email = Email;
            await _userManager.UpdateAsync(user);
            return Ok();
        }

        [HttpPost("PhoneNumber")]
        [Authorize]
        public async Task<IActionResult> UpdatePhoneNumber([FromForm] string Id, string phoneNumber)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            user.PhoneNumber = phoneNumber;
            await _userManager.UpdateAsync(user);
            return Ok();
        }

        [HttpPost("Address")]
        [Authorize]
        public async Task<IActionResult> UpdateAddress(string Id, AddressRequestAdd address)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            Address newAddress = new Address()
            {
                StreetAddress = address.StreetAddress,
                City = address.City,
                Province = address.Province,
                ZipCode = address.ZipCode
            };
            if (user.Addresses == null)
            {
                user.Addresses = _addressService.Add(newAddress).Result;
            }
            else
            {
                user.Addresses = _addressService.Update(newAddress).Result;
            }
            await _userManager.UpdateAsync(user);
            return Ok();
        }
    }
}
