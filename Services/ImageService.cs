using eTech.Context;
using eTech.Entities;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eTech.Services
{
  public class ImageService : IImageService
  {
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ImageService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
      _context = context;
      _webHostEnvironment = webHostEnvironment;
    }

    public Task<PhysicalFileResult> GetImage(string fileName)
    {
      var rootFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
      var filePath = Path.Combine(rootFolder, fileName);
      return Task.FromResult(new PhysicalFileResult(filePath, "image/jpeg"));
    }

    public Task<Image> Upload(IFormFile file)
    {
      if (file.Length <= 0)
      {
        return Task.FromResult(new Image());
      }
      var rootFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
      var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
      var filePath = Path.Combine(rootFolder, fileName);
      using (var stream = System.IO.File.Create(filePath))
      {
        file.CopyTo(stream);
        stream.Flush();
      }
      Image image = new Image
      {
        FileName = fileName,
        FilePath = "https://localhost:7066/static/images/" + fileName,
        FileSize = file.Length,
        OriginalFileName = file.FileName
      };
      return Task.FromResult(image);
    }

    public async Task DeleteImage(Image image)
    {
      if (image == null)
      {
        return;
      }
      var rootFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
      var filePath = Path.Combine(rootFolder, image.FileName);
      System.IO.File.Delete(filePath);
      _context.Images.Remove(image);
      await _context.SaveChangesAsync();
    }
  }
}
