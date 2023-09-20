using eTech.Context;
using eTech.Entities;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eTech.Services {
  public class RatingService : IRatingService {
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public RatingService(ApplicationDbContext context, UserManager<ApplicationUser> userManager) {
      _context = context;
      _userManager = userManager;
    }

    public Task<List<Rating>> GetAll() {
      return _context.Ratings.ToListAsync();
    }

    public Task<Rating> GetById(int id) {
      return _context.Ratings.FirstOrDefaultAsync(r => r.Id == id);
    }

    public Task<Rating> GetByProductId(int productId) {
      return _context.Ratings.FirstOrDefaultAsync(r => r.ProductId == productId);
    }

    public async Task<Rating> Add(Rating rating) {
      if(rating == null) {
        throw new ArgumentNullException(nameof(rating));
      }
      if(rating.Product == null && rating.ProductId != 0) {
        rating.Product = await _context.Products.FirstOrDefaultAsync(p => p.Id == rating.ProductId);
      }
      if(rating.User == null && rating.UserId != "") {
        rating.User = await _userManager.FindByIdAsync(rating.UserId);
      }
      _context.Ratings.Add(rating);
      await _context.SaveChangesAsync();
      return rating;
    }

    public async Task<Rating> Update(Rating rating) {
      if(rating.Id == 0) {
        return await Add(rating);
      }
      _context.Update(rating);
      await _context.SaveChangesAsync();
      return rating;
    }

    public async Task Delete(int id) {
      Rating rating = await _context.Ratings.FindAsync(id);
      _context.Remove(rating);
      await _context.SaveChangesAsync();
    }
  }
}
