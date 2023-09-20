namespace eTech.Entities.Requests {
  public class CategoryRequestAdd {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Product>? Products { get; set; }
    public IFormFile File { get; set; }
  }
}
