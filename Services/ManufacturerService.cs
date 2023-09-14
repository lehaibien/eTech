using eTech.Context;
using eTech.Entities;
using eTech.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eTech.Services;

public class ManufacturerService : IManufacturerService
{
  private readonly ApplicationDbContext _context;
  public ManufacturerService(ApplicationDbContext context)
  {
    _context = context;
  }
  public Task<List<Manufacturer>> GetAll()
  {
    return _context.Manufacturers.ToListAsync();
  }

  public Task<Manufacturer> GetById(int id)
  {
    return _context.Manufacturers.FirstOrDefaultAsync(manufacturer => manufacturer.Id == id);
  }

  public Task<Manufacturer> GetByProductId(int productId)
  {
    return _context.Manufacturers.FirstOrDefaultAsync(manufacturer => manufacturer.Products.Any(product => product.Id == productId));
  }
  public Task<Manufacturer> Add(Manufacturer manufacturer)
  {
    _context.Manufacturers.Add(manufacturer);
    _context.SaveChangesAsync();
    return Task.FromResult(manufacturer);
  }

  public Task<Manufacturer> Update(Manufacturer manufacturer)
  {
    _context.Update(manufacturer);
    _context.SaveChangesAsync();
    return Task.FromResult(manufacturer);
  }

  public Task Delete(int id)
  {
    Manufacturer manufacturer = _context.Manufacturers.Find(id);
    _context.Remove(manufacturer);
    _context.SaveChangesAsync();
    return Task.CompletedTask;
  }
}