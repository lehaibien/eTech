namespace eTech.Entities.Requests
{
  public class CategoryRequestUpdate
  {
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
  }
}