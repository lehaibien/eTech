using eTech.Entities;
using eTech.Entities.Requests;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eTech.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CartController : ControllerBase
  {
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
      _cartService = cartService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
      List<CartItem> cartItems = await _cartService.GetByUserId(userId);
      return Ok(cartItems);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(CartItemRequestAdd cartItem)
    {
      if (cartItem == null)
      {
        return BadRequest("Cart item is null");
      }
      CartItem cart = new()
      {
        UserId = cartItem.UserId,
        ProductId = cartItem.ProductId,
        Quantity = cartItem.Quantity,
      };
      return Ok(await _cartService.Add(cart));
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update(CartItemRequestUpdate cartItem)
    {
      if (cartItem == null)
      {
        return BadRequest("Cart item is null");
      }
      CartItem cart = new()
      {
        UserId = cartItem.UserId,
        ProductId = cartItem.ProductId,
        Quantity = cartItem.Quantity,
      };
      return Accepted(await _cartService.Update(cart));
    }

    [HttpDelete("{userId}/{productId}")]
    [Authorize]
    public async Task<IActionResult> Delete(string userId, int productId)
    {
      await _cartService.Delete(userId, productId);
      return NoContent();
    }
  }
}
