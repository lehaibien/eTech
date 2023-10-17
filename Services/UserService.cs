using eTech.Context;
using eTech.Entities;
using eTech.Entities.Requests;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace eTech.Services {
    public class UserService : IUserService {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;

        public UserService(ApplicationDbContext context, IImageService imageService) {
            _context = context;
            _imageService = imageService;
        }

        public Task<ApplicationUser> GetUserById(string id) {
            return _context.Users.Include(u => u.Image)
                        .Include(u => u.Address)
                        .Include(u => u.Orders)
                        .ThenInclude(od => od.OrderItems)
                        .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Image> UpdateImageUser(ApplicationUser user, Image img) {
            _context.Images.Add(img);
            user.Image = img;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return img;
        }
    }
}
