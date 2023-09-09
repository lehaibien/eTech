using System.ComponentModel.DataAnnotations;

namespace eTech.Entities
{
  public class Manufacturer
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public List<Product> Products { get; set; }
  }
}