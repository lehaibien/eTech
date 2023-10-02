using eTech.Entities;
using eTech.Entities.Requests;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eTech.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class RatingController : ControllerBase
  {
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
      _ratingService = ratingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      return Ok(await _ratingService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      return Ok(await _ratingService.GetById(id));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(RatingRequestAdd rating)
    {
      if (rating == null)
      {
        return BadRequest("Rating is null");
      }
      Rating r = new Rating
      {
        ProductId = rating.ProductId,
        UserId = rating.UserId,
        Rate = rating.Rate,
        Comment = rating.Comment
      };
      return Ok(await _ratingService.Add(r));
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update(Rating rating)
    {
      if (rating == null)
      {
        return BadRequest("Rating is null");
      }
      return Ok(await _ratingService.Update(rating));
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
      await _ratingService.Delete(id);
      return Ok();
    }
  }
}
