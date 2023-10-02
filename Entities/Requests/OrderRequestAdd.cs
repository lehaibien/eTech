using eTech.Enums;

namespace eTech.Entities.Requests
{
    public class OrderRequestAdd
    {
        public string UserId { get; set; }
        public PaymentRequestAdd Payment { get; set; }
        public OrderStatus Status { get; set; }
    }
}
