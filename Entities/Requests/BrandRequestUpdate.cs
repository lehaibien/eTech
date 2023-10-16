namespace eTech.Entities.Requests
{
  public class BrandRequestUpdate
  {
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Country { get; set; }
    public IFormFile Image { get; set; }
  }
}