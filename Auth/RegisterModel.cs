using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eTech.Auth {
  public class RegisterModel {
    [Required(ErrorMessage = "User Name is required")]
    public string? Username { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }
  }
}
