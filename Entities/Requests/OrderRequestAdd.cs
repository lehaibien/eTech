using eTech.Enums;

namespace eTech.Entities.Requests
{
  public class OrderRequestAdd
  {
    public string UserId { get; set; }
    public double Amount { get; set; }
    public PaymentType Type { get; set; }
    public double Fee { get; set; }
    public ICollection<OrderItemRequestAdd> Items { get; set; }
    public OrderStatus Status { get; set; }
  }
}
