using eTech.Entities;
using eTech.Entities.Requests;

namespace eTech.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<List<Order>> GetAll();
        public Task<Order> GetById(int Id);
        public Task<Order> Add(Order order);
        public Task<Order> AddOrderItem(OrderItemRequestAdd orderItemRequest);
        public Task RemoveOrderItem(int id, int orderId);
        public Task<Order> Update(Order order);
        public Task Delete(int id);

    }
}
