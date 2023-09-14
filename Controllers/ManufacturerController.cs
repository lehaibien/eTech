using eTech.Entities;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eTech.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ManufacturerController : ControllerBase
  {
    private readonly IManufacturerService _manufacturerService;
    public ManufacturerController(IManufacturerService manufacturerService)
    {
      _manufacturerService = manufacturerService;
    }

    [HttpGet]
    public Task<List<Manufacturer>> GetAll()
    {
      return _manufacturerService.GetAll();
    }

    [HttpPost]
    public Task<Manufacturer> Add(Manufacturer manufacturer)
    {
      manufacturer.Image = new ManufacturerImage();
      return _manufacturerService.Add(manufacturer);
    }

    [HttpPost]
    public Task<ManufacturerImage> Upload(IFormFile file)
    {
      ManufacturerImage image = new()
      {
        FileName = file.FileName,
        OriginalFileName = file.Name,
        FileSize = file.Length,
      };
      return Task.FromResult<ManufacturerImage>(image);
    }
  }
}