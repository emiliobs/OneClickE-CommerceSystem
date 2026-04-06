using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneClick.Shared.DTOs;

public class UserProfileDTO
{
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string? ImageBase64 { get; set; }
}