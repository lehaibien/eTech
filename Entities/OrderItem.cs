using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTech.Entities
{
  public class OrderItem
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int OrderId { get; set; }
    [ForeignKey(nameof(OrderId))]
    public virtual Order Order { get; set; }

    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; }

    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
  }
}