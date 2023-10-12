using eTech.Entities;

namespace eTech.Services.Interfaces {
    public interface ICartService {
        Task<List<CartItem>> GetAll();
        Task<CartItem> GetById(int Id);
        Task<List<CartItem>> GetByUserId(string userId);
        Task<CartItem> Add(CartItem cart);
        Task<CartItem> Update(CartItem cart);
        Task Delete(int id);
    }
}
