using eTech.Entities;

namespace eTech.Services.Interfaces;

public interface IManufacturerService
{
  public Task<List<Manufacturer>> GetAll();
  public Task<Manufacturer> GetById(int id);
  public Task<Manufacturer> GetByProductId(int productId);
  public Task<Manufacturer> Add(Manufacturer manufacturer);
  public Task<Manufacturer> Update(Manufacturer manufacturer);
  public Task Delete(int id);
}