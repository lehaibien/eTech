using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTech.Entities
{
  public class Product
  {
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }
    public int BrandId { get; set; }
    [ForeignKey(nameof(BrandId))]
    public Brand Brand { get; set; }
    public virtual ICollection<Image> Images { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
  }
}