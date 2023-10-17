using eTech.Entities;
using eTech.Entities.Requests;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eTech.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrderController : ControllerBase
  {
    private readonly IOrderService _orderService;
    private readonly ICartService _cartService;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrderController(IOrderService orderService, UserManager<ApplicationUser> userManager, ICartService cartService)
    {
      _orderService = orderService;
      _userManager = userManager;
      _cartService = cartService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
      return Ok(await _orderService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      return Ok(await _orderService.GetById(id));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(OrderRequestAdd orderRequest)
    {
      Payment payment = new()
      {
        Amount = orderRequest.Amount,
        Type = orderRequest.Type,
        Fee = orderRequest.Fee,
      };
      Order order = new()
      {
        UserId = orderRequest.UserId,
        Payment = payment,
        OrderItems = new List<OrderItem>(),
        OrderStatus = orderRequest.Status,
      };
      Order od = await _orderService.Add(order);
      List<OrderItem> orderItems = new();
      foreach (OrderItemRequestAdd item in orderRequest.Items)
      {
        OrderItem orderItem = new()
        {
          OrderId = od.Id,
          ProductId = item.ProductId,
          Quantity = item.Quantity,
        };
        orderItems.Add(orderItem);
      }
      foreach(var item in orderItems) {
        await _orderService.AddOrderItem(item);
      }
      //var tasks = orderItems.Select(async item => await _orderService.AddOrderItem(item));
      //await Task.WhenAll(tasks);
      List<CartItem> cartItems = await _cartService.GetByUserId(od.UserId);
      var cartTask = cartItems.Select(async item => await _cartService.Delete(od.UserId, item.ProductId));
      await Task.WhenAll(cartTask);
      return Ok(await _orderService.GetById(od.Id));
    }

    [HttpPut]
    [Authorize]
    public Task<Order> Update(Order order)
    {
      return _orderService.Update(order);
    }

    [HttpPut("{id}/status")]
    [Authorize]
    public async Task<Order> UpdateStatus(int id,[FromBody] int status)
    {
      Order order = await _orderService.GetById(id);
      order.OrderStatus = (Enums.OrderStatus)status;
      return await _orderService.Update(order);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
      await _orderService.Delete(id);
      return NoContent();
    }

    [HttpPost]
    [Route("OrderItem")]
    [Authorize]
    public Task<Order> CreateOrderItem([FromForm] OrderItem item)
    {
      return _orderService.AddOrderItem(item);
    }

    [HttpDelete]
    [Route("OrderItem")]
    public Task DeleteOrderItem(int id, int orderId)
    {
      return _orderService.RemoveOrderItem(id, orderId);
    }
  }
}
