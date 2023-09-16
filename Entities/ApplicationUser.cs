using Microsoft.AspNetCore.Identity;

namespace eTech.Entities
{
  public class ApplicationUser : IdentityUser
  {
    public string Name { get; set; }
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<Rating> Ratings { get; set; }
    public Image Image { get; set; }
  }
}