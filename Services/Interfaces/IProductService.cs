using eTech.Entities;

namespace eTech.Services.Interfaces {
  public interface IProductService {
    public Task<List<Product>> GetAll();
    public Task<Product> GetById(int id);
    public Task<Product> GetByBrandId(int brandId);
    public Task<Product> GetByCategoryId(int categoryId);
    public Task<Product> Add(Product product);
    public Task<Product> Update(Product product);
    public Task Delete(int id);
  }
}
