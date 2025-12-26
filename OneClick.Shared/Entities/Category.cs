using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneClick.Shared.Entities;

public class Category
{
    public int Id { get; set; }

    [MaxLength(100, ErrorMessage = "Field {0} cannot have more than {1} characters.")]
    [Required(ErrorMessage = "Field {0} is required.")]
    public string Name { get; set; } = null!;

    // Navigation property
    public List<Product> Products { get; set; } = new List<Product> { };
}