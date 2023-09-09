using Microsoft.AspNetCore.Identity;

namespace eTech.Entities
{
  public class ApplicationUser : IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<Order> Orders { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public UserImage UserImage { get; set; }
  }
}