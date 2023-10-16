using eTech.Entities;
using eTech.Entities.Requests;

namespace eTech.Services.Interfaces
{
    public interface IUserService
    {
        public Task<Image> UpdateImageUser(IFormFile file);
        public Task<Address> UpdateAddress(AddressRequestAdd address);
    }
}
