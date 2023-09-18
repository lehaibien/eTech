using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTech.Entities
{
  public class Address
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string ZipCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public string UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }
  }
}