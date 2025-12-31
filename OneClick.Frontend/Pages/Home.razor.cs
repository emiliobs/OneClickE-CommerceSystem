using OneClick.Shared.Entities;

namespace OneClick.Frontend.Pages;

public partial class Home
{
    // List to hold our fake data
    private List<Product> products = new List<Product>();

    private string? message;

    protected override void OnInitialized()
    {
        // 1. CREATE MOCK DATA (Datos Falsos para probar)
        products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "iPhone 15 Pro",
                Description = "Titanium design, A17 Pro chip.",
                ImageURL = "https://images.unsplash.com/photo-1551107696-a4b0c5a0d9a2?auto=format&fit=crop&w=600&q=80",
                Price = 999.00m,
                Qty = 20 // Green Badge
            },
            new Product
            {
                Id = 2,
                Name = "Samsung TV 4K",
                Description = "Crystal UHD resolution.",
                ImageURL = "https://images.unsplash.com/photo-1680519324888-03823798950c?auto=format&fit=crop&w=600&q=80",
                Price = 450.00m,
                Qty = 4 // Yellow/Red Badge
            },
            new Product
            {
                Id = 3,
                Name = "Nike Air Jordan",
                Description = "Classic basketball sneakers.",
                ImageURL = "https://images.unsplash.com/photo-1517336714731-489689fd1ca4?auto=format&fit=crop&w=600&q=80",
                Price = 120.00m,
                Qty = 1 // Red Badge (Last item!)
            },
            new Product
            {
                Id = 4,
                Name = "Old Laptop",
                Description = "Refurbished laptop from 2015.",
                ImageURL = "https://images.unsplash.com/photo-1696446701796-da61225697cc?auto=format&fit=crop&w=600&q=80",
                Price = 150.00m,
                Qty = 0 // Disabled Button (Out of Stock)
            }
        };
    }

    // 2. HANDLE THE EVENT (Lógica del Padre)
    private void HandleAddToCart(Product product)
    {
        // Logic: Decrease visual stock and show message
        if (product.Qty > 0)
        {
            product.Qty--;
            message = $"Added '{product.Name}' to your cart! Remaining: {product.Qty}";
        }
    }
}