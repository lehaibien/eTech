using System.ComponentModel.DataAnnotations;

namespace eTech.Entities
{
  public class Address
  {
    [Key]
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string ZipCode { get; set; }

    public ApplicationUser User { get; set; }
    public string ApplicationUserId { get; set; }
  }
}