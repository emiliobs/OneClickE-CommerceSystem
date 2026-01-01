using Microsoft.AspNetCore.Components;
using OneClick.Shared.Entities;

namespace OneClick.Frontend.Pages.Products;

public partial class ProductDetailModal
{
    [Parameter]
    public Product? Product { get; set; }

    [Parameter]
    public EventCallback OnClose { get; set; }

    [Parameter]
    public EventCallback<Product> OnAddToCart { get; set; }

    private async Task HandledAddToCart()
    {
        await OnAddToCart.InvokeAsync(Product);
        // Optional: Close modal after adding
        await OnClose.InvokeAsync();
    }
}