using eTech.Entities;
using eTech.Entities.Requests;
using eTech.Entities.Response;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
      return Ok(product);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
      if(String.IsNullOrEmpty(query)) {
        return BadRequest("Query is null");
      }
      // deserialize query
      List<Product> products = await _productService.Search(query);
      return Ok(products);
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
