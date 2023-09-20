using eTech.Context;
using eTech.Entities;
using eTech.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eTech.Services {
  public class RatingService : IRatingService {
    private readonly ApplicationDbContext _context;

    public RatingService(ApplicationDbContext context) {
      _context = context;
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
      _context.Add(rating);
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
