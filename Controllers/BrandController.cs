using eTech.Entities;
using eTech.Entities.Requests;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTech.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BrandController : ControllerBase
  {
    private readonly IBrandService _brandService;
    private readonly IImageService _imageService;
    public BrandController(IBrandService brandService, IImageService imageService)
    {
      _brandService = brandService;
      _imageService = imageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      return Ok(await _brandService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      return Ok(await _brandService.GetById(id));
    }

    [HttpPost]
    [Authorize]
    // Experimental
    public Task<Brand> Add([FromForm] BrandRequestAdd brandRequest)
    {
      Image image = _imageService.Upload(brandRequest.File).Result;
      Brand brand = new Brand()
      {
        Name = brandRequest.Name,
        Country = brandRequest.Country,
        Products = brandRequest.Products,
        Image = image
      };
      return _brandService.Add(brand);
    }

    [HttpPut]
    [Authorize]
    public Task<Brand> Update(Brand brand)
    {
      return _brandService.Update(brand);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public Task Delete(int id)
    {
      return _brandService.Delete(id);
    }
  }
}