using eTech.Entities;
using eTech.Entities.Requests;
using eTech.Services;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll() 
        {
            return Ok(await  _orderService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            return Ok(await _orderService.GetById(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<Order> Add([FromForm] OrderRequestAdd orderRequest)
        {
            var user = await _userManager.FindByIdAsync(orderRequest.UserId);

            Payment payment = new Payment()
            {
                Amount = orderRequest.Payment.Amount,
                Type = orderRequest.Payment.Type,
                Fee = orderRequest.Payment.Fee,
            };
            Order order = new Order()
            {
                UserId = orderRequest.UserId,
                User = user,
                Payment = payment,
                OrderStatus = orderRequest.Status,
            };
            return await _orderService.Add(order);
        }

        [HttpPut]
        [Authorize]
        public Task<Order> Update(Order order)
        {
            return _orderService.Update(order);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public Task Delete(int id)
        {
            return _orderService.Delete(id);
        }

        [HttpPost]
        [Route("OrderItem")]
        [Authorize]
        public Task<Order> CreateOrderItem([FromForm] OrderItemRequestAdd orderItemRequest)
        {
            return _orderService.AddOrderItem(orderItemRequest);
        }

        [HttpDelete]
        [Route("OrderItem")]
        public Task DeleteOrderItem(int id, int orderId)
        {
            return _orderService.RemoveOrderItem(id, orderId);
        }
    }
}
