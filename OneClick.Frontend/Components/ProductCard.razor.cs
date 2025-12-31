using Microsoft.AspNetCore.Components;
using OneClick.Shared.Entities;

namespace OneClick.Frontend.Components;

public partial class ProductCard
{
    [Parameter, EditorRequired]
    public Product Product { get; set; } = default!;

    [Parameter]
    public EventCallback<Product> OnAddToCart { get; set; }

    private async Task HandleClick()
    {
        if (OnAddToCart.HasDelegate)
        {
            await OnAddToCart.InvokeAsync(Product);
        }
    }
}