namespace eTech.Entities.Requests {
  public class ProductRequestAdd {
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public List<IFormFile> Images { get; set; }
  }
}
