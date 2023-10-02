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
      return _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task<Image> GetImageByCategoryId(int id)
    {
      return _context.Categories.Where(c => c.Id == id).Select(c => c.Image).FirstAsync();
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
      throw new NotImplementedException();
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
