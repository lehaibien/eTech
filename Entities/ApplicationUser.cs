using Microsoft.AspNetCore.Identity;

namespace eTech.Entities
{
  public class ApplicationUser : IdentityUser
  {
    public required string Name { get; set; }
    public virtual Address? Address { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public virtual Image? Image { get; set; }
  }
}