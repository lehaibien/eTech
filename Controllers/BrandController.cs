using eTech.Entities;
using eTech.Entities.Requests;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eTech.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BrandController : ControllerBase
  {
    private readonly IBrandService _brandService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public BrandController(IBrandService brandService, IWebHostEnvironment webHostEnvironment)
    {
      _brandService = brandService;
      _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public Task<List<Brand>> GetAll()
    {
      return _brandService.GetAll();
    }

    [HttpPost]
    // Experimental
    public Task<Brand> Add([FromForm] BrandRequestAdd brandRequest)
    {
      Image image = Upload(brandRequest.File).Result;
      Brand brand = new Brand()
      {
        Name = brandRequest.Name,
        Country = brandRequest.Country,
        Products = brandRequest.Products,
        Image = image
      };
      /*image.BrandId = brand.Id;
      image.Brand = brand;*/
      return _brandService.Add(brand);
    }

    [HttpPost("/upload")]
    public Task<Image> Upload(IFormFile file)
    {
      Image image = new();
      if (file.Length <= 0)
      {
        return Task.FromResult(image);
      }
      var rootFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
      var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
      var filePath = Path.Combine(rootFolder, fileName);
      using (var stream = System.IO.File.Create(filePath))
      {
        file.CopyTo(stream);
        stream.Flush();
      }
      image.FileName = fileName;
      image.FilePath = filePath;
      image.FileSize = file.Length;
      image.OriginalFileName = file.FileName;
      return Task.FromResult(image);
    }
  }
}