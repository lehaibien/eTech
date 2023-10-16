namespace eTech.Entities.Response {
    public class UserResponse {
        public string Id { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Name { get; set; }
        public List<Address> Address { get; set; }
        public Image Image { get; set; }
        public Address Address { get; set; }
        public ICollection<Order> Orders { get; set; }
        public string Role { get; set; }
    }
}
