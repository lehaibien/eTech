using eTech.Context;
using eTech.Entities;
using eTech.Services.Interfaces;

namespace eTech.Services {
  public class ImageService : IImageService {
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ImageService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment) {
      _context = context;
      _webHostEnvironment = webHostEnvironment;
    }

    public Task<Image> Upload(IFormFile file) {
      Image image = new();
      if (file.Length <= 0) {
        return Task.FromResult(image);
      }
      var rootFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
      var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
      var filePath = Path.Combine(rootFolder, fileName);
      using (var stream = System.IO.File.Create(filePath)) {
        file.CopyTo(stream);
        stream.Flush();
      }
      image.FileName = fileName;
      image.FilePath = filePath;
      image.FileSize = file.Length;
      image.OriginalFileName = file.FileName;
      return Task.FromResult(image);
    }

    public async Task DeleteImage(Image image) {
      if (image == null) {
        return;
      }
      System.IO.File.Delete(image.FilePath);
      _context.Images.Remove(image);
      await _context.SaveChangesAsync();
    }
  }
}
