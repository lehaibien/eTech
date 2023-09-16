namespace eTech.Entities.Requests
{
  public class ManufacturerRequestAdd
  {
    public string Name { get; set; }
    public string Country { get; set; }
    public List<Product>? Products { get; set; }
    public IFormFile File { get; set; }
  }
}