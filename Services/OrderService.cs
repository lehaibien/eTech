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

    public async Task<Order> AddOrderItem(OrderItem item)
    {
      item.Product = _context.Products.Find(item.ProductId);

      _context.OrderItems.Add(item);
      await _context.SaveChangesAsync();
      return _context.Orders.SingleOrDefault(o => o.Id == item.OrderId);
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
          .ThenInclude(u => u.Address)
          .Include(o => o.User)
          .ThenInclude(u => u.Image)
          .Include(o => o.Payment)
          //.Include(o => o.User.Addresses)
          .Include(o => o.OrderItems)
          .ThenInclude(o => o.Product)
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

    public async Task RemoveOrderItem(int productId, int orderId)
    {
      OrderItem orderItem = await _context.OrderItems.SingleOrDefaultAsync(oi => oi.ProductId == productId && oi.OrderId == orderId);
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
