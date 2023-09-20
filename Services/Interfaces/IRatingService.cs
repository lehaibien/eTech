using eTech.Entities;

namespace eTech.Services.Interfaces;

public interface IRatingService
{
  public Task<List<Rating>> GetAll();
  public Task<Rating> GetById(int id);
  public Task<Rating> GetByProductId(int productId);
  public Task<Rating> Add(Rating rating);
  public Task<Rating> Update(Rating rating);
  public Task Delete(int id);
}