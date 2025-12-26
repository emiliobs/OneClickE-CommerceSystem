using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneClick.Shared.Entities;

public class Product
{
    public int Id { get; set; }

    [MaxLength(100, ErrorMessage = "Field {0} cannot have more than {1} characters.")]
    [Required(ErrorMessage = "Field {0} is required.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [StringLength(500)]
    public string ImageURL { get; set; } = string.Empty;

    [Required(ErrorMessage = "Field {0} is required.")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Field {0} is required.")]
    [Range(0, int.MaxValue)]
    public int Qty { get; set; }

    [Required(ErrorMessage = "Field {0} is required.")]
    public int CategoryId { get; set; }

    // Navigation property
    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
}