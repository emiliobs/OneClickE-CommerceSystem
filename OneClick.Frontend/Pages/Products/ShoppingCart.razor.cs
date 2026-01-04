using Microsoft.AspNetCore.Components;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;

namespace OneClick.Frontend.Pages.Products;

public partial class ShoppingCart
{
    [Inject]
    public ICartService CartService { get; set; } = default!;

    [Parameter]
    public bool IsOpen { get; set; }

    [Parameter]
    public EventCallback<bool> IsOpenChanged { get; set; }

    private List<CartItem> cartItems = new List<CartItem>();
    private bool isLoading = true;

    protected override async Task OnParametersSetAsync()
    {
        if (IsOpen)
        {
            await LoadCart();
        }
    }

    private async Task LoadCart()
    {
        isLoading = true;
        try
        {
            // Use ID 1
            cartItems = await CartService.GetCartItemsAsync(1);
        }
        catch (Exception ex)
        {
            IsOpen = false;
            await IsOpenChanged.InvokeAsync(IsOpen);
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task CloseModal()
    {
        IsOpen = false;

        await IsOpenChanged.InvokeAsync(false);
    }
}