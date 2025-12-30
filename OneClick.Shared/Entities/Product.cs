using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneClick.Shared.Entities;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    [MaxLength(200, ErrorMessage = "Product name cannot exceed 200 characters")]
    public string Name { get; set; } = null!;

    [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string Description { get; set; } = null!;

    [MaxLength(500, ErrorMessage = "Image URL cannot exceed 500 characters")]
    [Url(ErrorMessage = "Please enter a valid URL")]
    [Required(ErrorMessage = "Image is required")]
    public string ImageURL { get; set; } = null!;

    [Required(ErrorMessage = "Field {0} is required.")]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
    public int Qty { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
    [Required(ErrorMessage = "Field {0} is required.")]
    public int CategoryId { get; set; }

    // Navigation property
    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
}