using eTech.Entities;
using eTech.Entities.Requests;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eTech.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class ProductController : ControllerBase {
    private readonly IProductService _productService;
    private readonly IImageService _imageService;

    public ProductController(IProductService productService, IImageService imageService) {
      _productService = productService;
      _imageService = imageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
      return Ok(await _productService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) {
      return Ok(await _productService.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] ProductRequestAdd product) {
      Product p = new Product{
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        Stock = product.Stock,
        BrandId = product.BrandId,
        CategoryId = product.CategoryId,
        Images = new List<Image>()
      };
      if (product == null) {
        return BadRequest("Product is null");
      }
      if(product.Images.Any()) {
        foreach (IFormFile image in product.Images) {
          Image img = await _imageService.Upload(image);
          p.Images.Add(img);
        }
      }
      return Ok(await _productService.Add(p));
    }
  }
}
