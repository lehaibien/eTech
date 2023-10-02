using eTech.Entities.Requests;
using eTech.Entities;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eTech.Services;
using Microsoft.AspNetCore.Identity;

namespace eTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly UserManager<ApplicationUser> _userManager;
        public AddressController(IAddressService addressService, UserManager<ApplicationUser> userManager)
        {
            _addressService = addressService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _addressService.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _addressService.GetById(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<Address> Add([FromForm] AddressRequestAdd addressRequest)
        {
            var user = await _userManager.FindByIdAsync(addressRequest.UserId);
            Address address = new Address()
            {
                Name = addressRequest.Name,
                StreetAddress = addressRequest.StreetAddress,
                City = addressRequest.City,
                Province = addressRequest.Province,
                ZipCode = addressRequest.ZipCode,
                UserId = addressRequest.UserId,
                User = user
            };

            return await _addressService.Add(address);
        }

        [HttpPut]
        [Authorize]
        public Task<Address> Update(Address address)
        {
            return _addressService.Update(address);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public Task Delete(int id)
        {
            return _addressService.Delete(id);
        }
    }
}
