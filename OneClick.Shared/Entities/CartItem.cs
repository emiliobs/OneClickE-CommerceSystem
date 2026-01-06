using System.ComponentModel.DataAnnotations;

namespace OneClick.Shared.Entities;

public class CartItem
{
    public int Id { get; set; }

    // Relationship: Belongs to a User
    public int UserId { get; set; }

    // NUEVO: Propiedad de navegación
    public User? User { get; set; }

    // Relationship: Links to a Product
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    // Validation: Cart cannot have 0 or negative items
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; } = 1;
}