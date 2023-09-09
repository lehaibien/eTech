using System.ComponentModel.DataAnnotations;

namespace eTech.Entities
{
  public class Rating
  {
    [Key]
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public double Rate { get; set; }
  }
}