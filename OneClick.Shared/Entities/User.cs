using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneClick.Shared.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        // This regex allows alphabetic characters (a-z, A-Z) and spaces, but no numbers.
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The name must contain only letters.")]
        public string Name { get; set; } = string.Empty;

        // Validation: Ensure it is a valid email format
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        // Note: In a real app, never store plain passwords. This stores the Hash.
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "Customer"; // "Admin" or "Customer"
    }
}