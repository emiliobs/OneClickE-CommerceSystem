using Microsoft.AspNetCore.Components;
using OneClick.Shared.Entities;

namespace OneClick.Frontend.Pages.Products;

public partial class ProductCard
{
    [Parameter, EditorRequired]
    public Product Product { get; set; } = default!;

    [Parameter]
    public EventCallback<Product> OnAddToCart { get; set; }

    [Parameter]
    public EventCallback<Product> OnViewDetails { get; set; }

    private async Task HandleClick()
    {
        if (OnAddToCart.HasDelegate)
        {
            await OnAddToCart.InvokeAsync(Product);
        }
    }

    private async Task HandledViewDetails()
    {
        if (OnViewDetails.HasDelegate)
        {
            await OnViewDetails.InvokeAsync(Product);
        }
    }
}