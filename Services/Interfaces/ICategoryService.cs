using eTech.Entities;

namespace eTech.Services.Interfaces;

public interface ICategoryService
{
  public Task<List<Category>> GetAll();
  public Task<Category> GetById(int id);
  public Task<Category> GetByProductId(int productId);
  public Task<Image> GetImageByCategoryId(int id);
  public Task<Category> Add(Category category);
  public Task<Category> Update(Category category);
  public Task Delete(int id);
}