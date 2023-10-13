using eTech.Entities;
using Microsoft.AspNetCore.Mvc;

namespace eTech.Services.Interfaces
{
  public interface IImageService
  {
    public Task<PhysicalFileResult> GetImage(string fileName);
    public Task<Image> Upload(IFormFile file);
    public Task DeleteImage(Image image);
  }
}
