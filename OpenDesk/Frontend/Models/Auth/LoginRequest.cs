using System.ComponentModel.DataAnnotations;

namespace Frontend.Models.Auth;

public class LoginRequest
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = string.Empty;
    public string TwoFactorCode { get; set; } = default!;
    public string TwoFactorRecoveryCode { get; set; } = default!;

    public bool Success { get; set; } = false;
    public string ErrorMessage { get; set; } = string.Empty;
}