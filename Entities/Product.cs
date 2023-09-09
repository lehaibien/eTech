using System.ComponentModel.DataAnnotations;

namespace eTech.Entities
{
  public class Product
  {
    public Product()
    {
      CreatedAt = DateTime.UtcNow;
      ModifiedAt = DateTime.UtcNow;
    }
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int ManufacturerId { get; set; }
    public Manufacturer Manufacturer { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime ModifiedAt { get; set; }
  }
}