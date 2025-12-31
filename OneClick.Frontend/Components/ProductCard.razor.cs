using Microsoft.AspNetCore.Components;

namespace OneClick.Frontend.Components;

public partial class ProductCard
{
    // Parameters allow us to pass data from the parent component into this card
    [Parameter]
    public string Title { get; set; } = "";

    [Parameter]
    public string Category { get; set; } = "";

    [Parameter]
    public string ImageUrl { get; set; } = "";

    [Parameter]
    public decimal Price { get; set; }

    [Parameter]
    public int StockUnits { get; set; }

    [Parameter]
    public bool IsOnSale { get; set; }

    [Parameter]
    public bool IsNew { get; set; }

    // Event callback to notify the parent component when the button is clicked
    [Parameter]
    public EventCallback<string> OnAddToCartClicked { get; set; }

    private async Task AddToCart()
    {
        // We trigger the event sending the product Title (or ID) back to the parent
        if (OnAddToCartClicked.HasDelegate)
        {
            await OnAddToCartClicked.InvokeAsync(Title);
        }
    }
}