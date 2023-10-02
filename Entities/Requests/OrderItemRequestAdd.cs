namespace eTech.Entities.Requests
{
    public class OrderItemRequestAdd
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
