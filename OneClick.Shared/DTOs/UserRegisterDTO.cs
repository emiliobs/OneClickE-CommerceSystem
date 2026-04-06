using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneClick.Shared.DTOs;

public class UserRegisterDTO
{
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password must be at least 3 characters")]
    [MinLength(3)]
    public string Password { get; set; } = string.Empty;

    public string? ImageBase64 { get; set; }
}