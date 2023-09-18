using eTech.Entities;

namespace eTech.Services.Interfaces;

public interface IBrandService
{
  public Task<List<Brand>> GetAll();
  public Task<Brand> GetById(int id);
  public Task<Brand> GetByProductId(int productId);
  public Task<Brand> Add(Brand brand);
  public Task<Brand> Update(Brand brand);
  public Task Delete(int id);
}