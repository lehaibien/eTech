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
  public class CategoryController : ControllerBase
  {
    private readonly ICategoryService _categoryService;
    private readonly IImageService _imageService;

    public CategoryController(ICategoryService categoryService, IImageService imageService)
    {
      _categoryService = categoryService;
      _imageService = imageService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
      return Ok(await _categoryService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      return Ok(await _categoryService.GetById(id));
    }

    [HttpGet("{id}/image")]
    public Task<Image> GetImage(int id)
    {
      return _categoryService.GetImageByCategoryId(id);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add([FromForm] CategoryRequestAdd categoryRequest)
    {
      Image image = await _imageService.Upload(categoryRequest.File);
      Category cat = new Category
      {
        Name = categoryRequest.Name,
        Description = categoryRequest.Description,
        Products = categoryRequest.Products,
        Image = image
      };
      Category response = null;
      try
      {
        response = await _categoryService.Add(cat);
      }
      catch
      {
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return CreatedAtAction(nameof(Add), response);
    }
  }
}
