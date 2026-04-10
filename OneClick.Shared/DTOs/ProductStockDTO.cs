using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneClick.Shared.DTOs;

//
public class ProductStockDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CurrentStock { get; set; }

    [Range(1, 10000, ErrorMessage = "Please enter a valid restock quantity.")]
    public int QuantityToAdd { get; set; }

    public string? ImageURL { get; set; }

    // Logic to determine if the stock is in a "Critical" state (e.g., less than 6 units)
    public bool IsCritical => CurrentStock <= 5;
}