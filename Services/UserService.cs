using eTech.Context;
using eTech.Entities;
using eTech.Entities.Requests;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace eTech.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;

        public UserService(ApplicationDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }
        public Task<Image> UpdateImageUser(IFormFile file)
        {
            Image img = _imageService.Upload(file).Result;
            _context.Images.Add(img);
            _context.SaveChanges();
            return Task.FromResult(img);
        }
    }
}
