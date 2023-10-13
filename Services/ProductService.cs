using eTech.Context;
using eTech.Entities;
using eTech.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eTech.Services
{
  public class ProductService : IProductService
  {
    private readonly IImageService _imageService;
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context, IImageService imageService)
    {
      _context = context;
      _imageService = imageService;
    }

    public async Task<List<Product>> GetAll()
    {
      return await _context.Products.Include(p => p.Category).Include(p => p.Brand).Include(p => p.Images).ToListAsync();
    }

    public async Task<Product> GetById(int id)
    {
      return await _context.Products.Include(p => p.Images).Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<Product> GetByBrandId(int brandId)
    {
      return _context.Products.FirstOrDefaultAsync(p => p.BrandId == brandId);
    }

    public Task<Product> GetByCategoryId(int categoryId)
    {
      return _context.Products.FirstOrDefaultAsync(p => p.CategoryId == categoryId);
    }

    public async Task<List<Product>> Search(string query)
    {
      return await _context.Products.Include(p => p.Images).Where(p => p.Name.ToLower().Contains(query)).ToListAsync();
    }

    public async Task<Product> Add(Product product)
    {
      if (product == null)
      {
        throw new ArgumentNullException(nameof(product));
      }
      if (product.Brand == null && product.BrandId != 0)
      {
        product.Brand = _context.Brands.FirstOrDefault(b => b.Id == product.BrandId);
      }
      if (product.Category == null && product.CategoryId != 0)
      {
        product.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == product.CategoryId);
        product.Category.Products.Add(product);
      }
      _context.Add(product);
      await _context.SaveChangesAsync();
      return product;
    }

    public async Task<Product> Update(Product product)
    {
      if (product.Id == 0)
      {
        return await Add(product);
      }
      _context.Update(product);
      await _context.SaveChangesAsync();
      return product;
    }

    public async Task Delete(int id)
    {
      Product product = _context.Products.Include(p => p.Images).FirstOrDefault(p => p.Id == id);
      foreach (Image i in product.Images)
      {
        await _imageService.DeleteImage(i);
      }
      _context.Remove(product);
      await _context.SaveChangesAsync();
    }
  }
}
