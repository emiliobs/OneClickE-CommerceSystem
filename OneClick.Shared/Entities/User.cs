using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OneClick.Shared.Entities;

// Inheriting from IdentityUser<int> provides the integer ID, Email, and Password securely
public class User : IdentityUser<int>
{
    [Required(ErrorMessage = "First name is required.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The first name must contain only letters.")]
    public string FirstName { get; set; } = string.Empty;

    // --- NEW: Last Name property with identical validation ---
    [Required(ErrorMessage = "Last name is required.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The last name must contain only letters.")]
    public string LastName { get; set; } = string.Empty;

    // Keep the Role property for authorization later
    public string Role { get; set; } = "Customer";

    // Inverse relationships for Entity Framework
    public ICollection<Order>? Orders { get; set; }

    public ICollection<CartItem>? CartItems { get; set; }
}