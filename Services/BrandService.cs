using eTech.Context;
using eTech.Entities;
using eTech.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eTech.Services;

public class BrandService : IBrandService
{
  private readonly ApplicationDbContext _context;
  public BrandService(ApplicationDbContext context)
  {
    _context = context;
  }
  public Task<List<Brand>> GetAll()
  {
    return _context.Brands.ToListAsync();
  }

  public Task<Brand> GetById(int id)
  {
    return _context.Brands.FirstOrDefaultAsync(brand => brand.Id == id);
  }

  public Task<Brand> GetByProductId(int productId)
  {
    return _context.Brands.FirstOrDefaultAsync(brand => brand.Products.Any(product => product.Id == productId));
  }
  public Task<Brand> Add(Brand brand)
  {
    _context.Brands.Add(brand);
    _context.SaveChangesAsync();
    return Task.FromResult(brand);
  }

  public Task<Brand> Update(Brand brand)
  {
    _context.Update(brand);
    _context.SaveChangesAsync();
    return Task.FromResult(brand);
  }

  public Task Delete(int id)
  {
    Brand brand = _context.Brands.Find(id);
    _context.Remove(brand);
    _context.SaveChangesAsync();
    return Task.CompletedTask;
  }
}