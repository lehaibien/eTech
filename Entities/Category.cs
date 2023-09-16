using System.ComponentModel.DataAnnotations;

namespace eTech.Entities
{
  public class Category
  {
    public Category()
    {
      Products = new HashSet<Product>();
      CreatedAt = DateTime.UtcNow;
      ModifiedAt = DateTime.UtcNow;
    }
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public ICollection<Product> Products { get; set; }
    public Image Image { get; set; }
  }
}