using eTech.Context;
using eTech.Entities;
using eTech.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eTech.Services;

public class BrandService : IBrandService
{
  private readonly ApplicationDbContext _context;
  private readonly IImageService _imageService;
  public BrandService(ApplicationDbContext context, IImageService imageService) {
    _context = context;
    _imageService = imageService;
  }
  public Task<List<Brand>> GetAll()
  {
    return _context.Brands.Include(b => b.Image).ToListAsync();
  }

  public Task<Brand> GetById(int id)
  {
    return _context.Brands.FirstOrDefaultAsync(brand => brand.Id == id);
  }

  public Task<Brand> GetByProductId(int productId)
  {
    return _context.Brands.FirstOrDefaultAsync(brand => brand.Products.Any(p => p.Id == productId));
  }
  public async Task<Brand> Add(Brand brand)
  {
    _context.Images.Add(brand.Image);
    _context.Brands.Add(brand);
    await _context.SaveChangesAsync();
    return brand;
  }

  public Task<Brand> Update(Brand brand)
  {
    _context.Update(brand);
    _context.SaveChangesAsync();
    return Task.FromResult(brand);
  }

  public async Task Delete(int id)
  {
    Brand brand = _context.Brands.Find(id);
    await _imageService.DeleteImage(brand.Image);
    _context.Remove(brand);
    await _context.SaveChangesAsync();
  }
}