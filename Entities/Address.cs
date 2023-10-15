using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTech.Entities
{
  public class Address
  {
    [Key]
    public int Id { get; set; }
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string ZipCode { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
  }
}