using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneClick.Shared.DTOs;

// Using Records creates lightweight, immutable objects perfect for transferring data securely
// This record handles the data needed to create a brand new account
public record UserRegisterDTO
(
    [Required(ErrorMessage = "First name is required")]
    string FirstName,
    [Required(ErrorMessage = "Last name is required")]
    string LastName,
    [Required(ErrorMessage = "Email is required"), EmailAddress]
    string Email,
    [Required(ErrorMessage = "Password must be at least 6 characters"), MinLength(6)]
    string Password
);

// A smaller, focused record used only when the user wants to log in
public record UserLoginDTO
(
    [Required(ErrorMessage = "Email is required"),
    EmailAddress] string Email,
    [Required(ErrorMessage = "Password is required")]
    string Password
);