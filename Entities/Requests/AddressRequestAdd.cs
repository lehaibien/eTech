namespace eTech.Entities.Requests
{
    public class AddressRequestAdd
    {
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string? ZipCode { get; set; }
        public string UserId { get; set; }
    }
}
