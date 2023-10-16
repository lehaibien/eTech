namespace eTech.Entities.Requests
{
  public class CategoryRequestAdd
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
  }
}
