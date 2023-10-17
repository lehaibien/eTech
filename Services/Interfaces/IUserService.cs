using eTech.Entities;
using eTech.Entities.Requests;

namespace eTech.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ApplicationUser> GetUserById(string id);
        public Task<Image> UpdateImageUser(ApplicationUser user, Image img);
    }
}
