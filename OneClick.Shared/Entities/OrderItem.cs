using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OneClick.Shared.Entities;

public class OrderItem
{
    public int Id { get; set; }

    // Relationship: Link to the Parent Order
    public int OrderId { get; set; }

    // Important: JsonIgnore prevents infinite loops when sending data to Frontend
    [JsonIgnore]
    public Order? Order { get; set; }

    // Relationship: Link to the Product sold
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    // Validation: Must be positive
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public int Quantity { get; set; }

    // Validation: Historical Price (Snapshot of price at purchase time)
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }
}