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
    public async Task<IActionResult> GetImage(int id)
    {
      Category category = await _categoryService.GetById(id);
      if (category == null)
      {
        return NotFound();
      }
      return await _imageService.GetImage(category.Image.FileName);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add([FromForm] CategoryRequestAdd categoryRequest)
    {
      Image image = await _imageService.Upload(categoryRequest.Image);
      Category cat = new()
      {
        Name = categoryRequest.Name,
        Description = categoryRequest.Description,
        Products = new List<Product>(),
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

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update([FromForm] CategoryRequestUpdate category)
    {
      Category currentCategory = await _categoryService.GetById(category.Id);
      if (currentCategory == null)
      {
        return BadRequest("Category not found");
      }
      currentCategory.Name = category.Name;
      currentCategory.Description = category.Description;
      Image img = currentCategory.Image;
      if (category.Image != null && currentCategory.Image.OriginalFileName != category.Image.FileName)
      {
        Image image = _imageService.Upload(category.Image).Result;
        if (image == null)
        {
          return BadRequest("Image upload failed");
        }

        currentCategory.Image = image;
      }
      Category updatedCategory = await _categoryService.Update(currentCategory);
      if (updatedCategory.Image.FileName != img.FileName)
      {
        await _imageService.DeleteImage(img);
      }
      return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
      Category category = await _categoryService.GetById(id);
      if (category == null)
      {
        return BadRequest("Category not found");
      }
      await _categoryService.Delete(id);
      return Ok();
    }
  }
}