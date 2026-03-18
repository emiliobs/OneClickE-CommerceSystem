using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneClick.Shared.Entities;

public class Order
{
    public int Id { get; set; }

    // Relationship: Who placed the order
    public int UserId { get; set; }

    // NUEVO: Propiedad de navegación
    public User? User { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    //Configuration: Precise decimal for money (SQL won't lose cents)
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    public string OrderStatus { get; set; } = "Pending";

    [Required(ErrorMessage = "Full Name is required.")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string ShippingName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
    public string ShippingPhone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Shipping Address is required.")]
    [MinLength(5, ErrorMessage = "Please enter a full address.")]
    [MaxLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
    public string ShippingAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is required.")]
    [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Postcode is required.")]
    // Regex for UK Postcodes (e.g., SW1A 1AA, N7 8DB)
    [RegularExpression(@"^([A-Z]{1,2}\d[A-Z\d]? ?\d[A-Z]{2}|GIR ?0A{2})$", ErrorMessage = "Please enter a valid UK Postcode (e.g., N7 8DB).")]
    public string Postcode { get; set; } = string.Empty;

    // Relationship: One Order has Many Items
    public List<OrderItem> OrderItems { get; set; } = new();
}