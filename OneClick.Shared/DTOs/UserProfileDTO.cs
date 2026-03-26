using System;
using System.Collections.Generic;
using System.Text;

namespace OneClick.Shared.DTOs;

public class UserProfileDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public String Email { get; set; } = null!;
    public string Role { get; set; } = null!;
}