namespace eTech.Entities.Response
{
  public class ProductResponse
  {
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public Category Category { get; set; }
    public Brand Brand { get; set; }
    public virtual List<string> Images { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
  }
}