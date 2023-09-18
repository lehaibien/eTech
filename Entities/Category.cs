using System.ComponentModel.DataAnnotations;

namespace eTech.Entities
{
  public class Category
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public Image Image { get; set; }
  }
}