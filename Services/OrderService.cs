using eTech.Context;
using eTech.Entities;
using eTech.Entities.Requests;
using eTech.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eTech.Services
{
    public class OrderService : IOrderService
    {

        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Order> Add(Order order)
        {
            _context.Payments.Add(order.Payment);
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public async Task<Order>AddOrderItem(OrderItemRequestAdd orderItemRequest)
        {
            Order order = _context.Orders.Find(orderItemRequest.OrderId);
            Product product = _context.Products.Find(orderItemRequest.ProductId);

            OrderItem orderItem = new OrderItem()
            {
                OrderId = orderItemRequest.OrderId,
                Order = order,
                ProductId = orderItemRequest.ProductId,
                Product = product,
                Quantity = orderItemRequest.Quantity,
            };

            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();
            return _context.Orders.SingleOrDefault(o => o.Id == orderItemRequest.OrderId);
        }

        public async Task Delete(int id)
        {
            Order order = _context.Orders.Find(id);
            _context.Remove(order);
            await _context.SaveChangesAsync();
        }

        public Task<List<Order>> GetAll()
        {
            return _context.Orders
                .Include(o => o.User)
                .Include(o => o.Payment)
                //.Include(o => o.User.Addresses)
                .Include(o => o.OrderItems)
                .ToListAsync();
        }

        public Task<Order> GetById(int Id)
        {
            return _context.Orders
                .Include(a => a.User)
                .Include(p => p.Payment)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(od => od.Id == Id);
        }

        public async Task RemoveOrderItem(int id, int orderId)
        {
            OrderItem orderItem = await _context.OrderItems.SingleOrDefaultAsync(oi => oi.Id == id && oi.OrderId == orderId);
            _context.Remove(orderItem);
            await _context.SaveChangesAsync();
        }

        public Task<Order> Update(Order order)
        {
            _context.Update(order);
            _context.SaveChanges();
            return Task.FromResult(order);
        }
    }
}
