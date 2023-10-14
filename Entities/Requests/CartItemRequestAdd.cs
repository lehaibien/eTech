namespace eTech.Entities.Requests {
    public class CartItemRequestAdd {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
