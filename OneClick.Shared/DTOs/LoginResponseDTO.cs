using System;
using System.Collections.Generic;
using System.Text;

namespace OneClick.Shared.DTOs;

public class LoginResponseDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public string Message { get; set; } = string.Empty;
}