using System.ComponentModel.DataAnnotations;
using eTech.Enums;

namespace eTech.Entities
{
  public class Payment
  {
    [Key]
    public int Id { get; set; }
    public double Amount { get; set; }
    public PaymentType Type { get; set; }
    public double Fee { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
  }
}