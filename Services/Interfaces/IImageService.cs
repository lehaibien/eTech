using eTech.Entities;

namespace eTech.Services.Interfaces {
  public interface IImageService {
    public Task<Image> Upload(IFormFile file);
    public Task DeleteImage(Image image);
  }
}
