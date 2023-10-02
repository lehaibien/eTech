using eTech.Entities;
using eTech.Entities.Requests;
using eTech.Entities.Response;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTech.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private readonly IProductService _productService;
    private readonly IImageService _imageService;

    public ProductController(IProductService productService, IImageService imageService)
    {
      _productService = productService;
      _imageService = imageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      return Ok(await _productService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      Product product = await _productService.GetById(id);
      if (product == null)
      {
        return NotFound();
      }
      ProductResponse p = new()
      {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        Stock = product.Stock,
        Category = product.Category,
        Brand = product.Brand,
        Images = product.Images.Select(i => $"https://localhost:7066/static/images/{i.FileName}").ToList(),
        CreatedAt = product.CreatedAt,
        ModifiedAt = product.ModifiedAt
      };
      return Ok(p);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add([FromForm] ProductRequestAdd product)
    {
      if (product == null) {
        return BadRequest("Product is null");
      }
      Product p = new()
      {
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        Stock = product.Stock,
        BrandId = product.BrandId,
        CategoryId = product.CategoryId,
        Images = new List<Image>()
      };
      if (product.Images.Any())
      {
        foreach (IFormFile image in product.Images)
        {
          Image img = await _imageService.Upload(image);
          p.Images.Add(img);

        }
      }
      return Ok(await _productService.Add(p));
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update(Product product)
    {
      if (product == null)
      {
        return BadRequest("Product is null");

      }

      return Ok(await _productService.Update(product));
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
      await _productService.Delete(id);
      return Ok();

    }
  }
}
