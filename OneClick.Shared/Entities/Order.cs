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

    //Validation: We must know where to ship the package
    [Required(ErrorMessage = "Shipping Address is required.")]
    [MinLength(5, ErrorMessage = "Please enter a full address.")]
    public string ShippingAddress { get; set; } = string.Empty;

    // Relationship: One Order has Many Items
    public List<OrderItem> OrderItems { get; set; } = new();
}