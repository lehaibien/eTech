using eTech.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eTech.Entities
{
  public class ApplicationUser : IdentityUser
  {
    public required string Name { get; set; }
    public Address? Address { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public Image? Image { get; set; }
  }
}