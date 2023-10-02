using eTech.Enums;

namespace eTech.Entities.Requests
{
    public class PaymentRequestAdd
    {
        public double Amount { get; set; }
        public PaymentType Type { get; set; }
        public double Fee { get; set; }
    }
}
