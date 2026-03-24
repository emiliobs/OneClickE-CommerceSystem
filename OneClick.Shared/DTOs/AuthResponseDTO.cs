using System;
using System.Collections.Generic;
using System.Text;

namespace OneClick.Shared.DTOs;

// Keeping this as a record because we only receive it from the API, we do not edit it
public record AuthResponseDTO(string Token, DateTime Expiration, string Message);