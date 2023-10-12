using Microsoft.AspNetCore.Identity;

namespace eTech.Auth {
  public class AuthResponse {
    public string? Status { get; set; }
    public string? Message { get; set; }
    public IdentityResult? Result { get; set; }
  }
}
