using System.ComponentModel.DataAnnotations;
using eTech.Enums;

namespace eTech.Entities
{
  public class PaymentMethod
  {
    [Key]
    public int Id { get; set; }
    public PaymentType Type { get; set; }
    public string CardNumber { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CVV { get; set; }
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
  }
}