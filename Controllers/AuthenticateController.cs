using eTech.Auth;
using eTech.Entities;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eTech.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService) {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model) {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null) {
                return NotFound();
            }
            if (!(await _userManager.CheckPasswordAsync(user, model.Password))) {
                return Unauthorized();
            }

            string accessToken = _tokenService.GenerateAccessToken(user);
            return Ok(new {
                accessToken = accessToken,
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model) {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new() {
                Email = model.Email,
                UserName = model.Username,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) {
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User creation failed! Please check user details and try again.", Result = result });
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (await _roleManager.RoleExistsAsync(UserRoles.User)) {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model) {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null) {
                return StatusCode(StatusCodes.Status406NotAcceptable, new AuthResponse { Status = "Error", Message = "User already exists!" });
            }

            ApplicationUser user = new() {
                Email = model.Email,
                UserName = model.Username,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) {
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin)) {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.User)) {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
        }
    }
}
