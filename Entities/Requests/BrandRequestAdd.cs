namespace eTech.Entities.Requests
{
  public class BrandRequestAdd
  {
    public string Name { get; set; }
    public string Country { get; set; }
    public IFormFile Image { get; set; }
  }
}