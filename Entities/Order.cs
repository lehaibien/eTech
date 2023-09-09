using System.ComponentModel.DataAnnotations;
using eTech.Enums;

namespace eTech.Entities
{
  public class Order
  {
    public Order()
    {
      CreatedAt = DateTime.UtcNow;
      ModifiedAt = DateTime.UtcNow;
    }
    [Key]
    public int Id { get; set; }
    [Required]
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }
    public OrderStatus OrderStatus { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime ModifiedAt { get; set; }
  }
}