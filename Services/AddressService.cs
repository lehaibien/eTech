using eTech.Context;
using eTech.Entities;
using eTech.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eTech.Services
{
  public class AddressService : IAddressService
  {
    private readonly ApplicationDbContext _context;

    public AddressService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<Address> Add(Address address)
    {
      _context.Addresses.Add(address);
      await _context.SaveChangesAsync();
      return address;
    }

    public async Task Delete(int id)
    {
      Address address = _context.Addresses.Find(id);
      _context.Remove(address);
      await _context.SaveChangesAsync();
    }

    public Task<List<Address>> GetAll()
    {
      return _context.Addresses.ToListAsync();
    }

    public Task<Address> GetById(int id)
    {
      return _context.Addresses.FirstOrDefaultAsync(ad => ad.Id == id);
    }

    public Task<Address> Update(Address address)
    {
      _context.Update(address);
      _context.SaveChangesAsync();
      return Task.FromResult(address);
    }
  }
}
