using System.ComponentModel.DataAnnotations;

namespace eTech.Entities
{
  public class Brand
  {
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Country { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
    public Image Image { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
  }
}