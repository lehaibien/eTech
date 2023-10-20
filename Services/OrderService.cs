using eTech.Context;
using eTech.Entities;
using eTech.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eTech.Services {
    public class OrderService : IOrderService {

        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context) {
            _context = context;
        }
        public async Task<Order> Add(Order order) {
            _context.Payments.Add(order.Payment);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> AddOrderItem(OrderItem item) {
            item.Product = await _context.Products.FindAsync(item.ProductId);

            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();
            return await _context.Orders.SingleOrDefaultAsync(o => o.Id == item.OrderId);
        }

        public async Task Delete(int id) {
            Order order = await _context.Orders.Include(ord => ord.Payment).FirstOrDefaultAsync(ord => ord.Id == id);
            _context.Payments.Remove(order.Payment);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAll() {
            return await _context.Orders
                .Include(o => o.User)
                .ThenInclude(u => u.Address)
                .Include(o => o.Payment)
                //.Include(o => o.User.Addresses)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Product)
                .ThenInclude(o => o.Brand)
                .ThenInclude(p => p.Products)
                .ThenInclude(p => p.Images)
                .ToListAsync();
        }

        public async Task<Order> GetById(int Id) {
            Order ord = await _context.Orders
                .Include(a => a.User)
                .ThenInclude(u => u.Address)
                .Include(p => p.Payment)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(od => od.Id == Id);
            return ord;
        }

        public async Task RemoveOrderItem(int productId, int orderId) {
            OrderItem orderItem = await _context.OrderItems
              .SingleOrDefaultAsync(oi => oi.ProductId == productId && oi.OrderId == orderId);
            _context.Remove(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> Update(Order order) {
            _context.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
