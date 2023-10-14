using eTech.Context;
using eTech.Entities;
using eTech.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eTech.Services
{
  public class CategoryService : ICategoryService
  {
    private readonly ApplicationDbContext _context;
    private readonly IImageService _imageService;

    public CategoryService(ApplicationDbContext context, IImageService imageService)
    {
      _context = context;
      _imageService = imageService;
    }

    public Task<List<Category>> GetAll()
    {
      return _context.Categories.Include(c => c.Products).Include(c => c.Image).ToListAsync();
    }

    public Task<Category> GetById(int id)
    {
      return _context.Categories.Include(c => c.Image).Include(c => c.Products).ThenInclude(p => p.Images).Include(c => c.Products).ThenInclude(p => p.Brand).FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task<Category> GetByProductId(int productId)
    {
      return _context.Categories.FirstOrDefaultAsync(c => c.Products.Any(p => p.Id == productId));
    }

    public async Task<Category> Add(Category category)
    {
      _context.Images.Add(category.Image);
      _context.Categories.Add(category);
      await _context.SaveChangesAsync();
      return category;
    }

    public Task<Category> Update(Category category)
    {
      _context.Images.Update(category.Image);
      _context.SaveChangesAsync();
      _context.Update(category);
      _context.SaveChangesAsync();
      return Task.FromResult(category);
    }

    public async Task Delete(int id)
    {
      Category cat = _context.Categories.Find(id);
      _imageService.DeleteImage(cat.Image);
      _context.Remove(cat);
      await _context.SaveChangesAsync();
    }
  }
}
