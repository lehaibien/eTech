using eTech.Entities;
using eTech.Entities.Requests;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eTech.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ManufacturerController : ControllerBase
  {
    private readonly IManufacturerService _manufacturerService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ManufacturerController(IManufacturerService manufacturerService, IWebHostEnvironment webHostEnvironment)
    {
      _manufacturerService = manufacturerService;
      _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public Task<List<Manufacturer>> GetAll()
    {
      return _manufacturerService.GetAll();
    }

    [HttpPost]
    // Experimental
    public Task<Manufacturer> Add([FromForm] ManufacturerRequestAdd manufacturerRequest)
    {
      ManufacturerImage image = Upload(manufacturerRequest.File).Result;
      Manufacturer manufacturer = new Manufacturer()
      {
        Name = manufacturerRequest.Name,
        Country = manufacturerRequest.Country,
        Products = manufacturerRequest.Products,
        Image = image
      };
      /*image.ManufacturerId = manufacturer.Id;
      image.Manufacturer = manufacturer;*/
      return _manufacturerService.Add(manufacturer);
    }

    [HttpPost("/upload")]
    public Task<ManufacturerImage> Upload(IFormFile file)
    {
      ManufacturerImage image = new();
      if (file.Length <= 0)
      {
        return Task.FromResult(image);
      }
      var rootFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Images");
      // Add Folder Images If this not Exists
      if (!Directory.Exists(rootFolder))
      {
        Directory.CreateDirectory(rootFolder);
      }
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