using Microsoft.AspNetCore.Components;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;

namespace OneClick.Frontend.Pages.Products;

public partial class ShoppingCart
{
    [Inject]
    public ICartService CartService { get; set; } = default!;

    [Inject]
    public AlertService SweetAlertService { get; set; } = default!;

    [Parameter]
    public bool IsOpen { get; set; }

    [Parameter]
    public EventCallback<bool> IsOpenChanged { get; set; }

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;

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

    private async Task UpdateCartItemQuantity(CartItem cartItem, int change)
    {
        // Calculate new quantity
        int newQty = cartItem.Quantity + change;

        // Prevention: Do not go below 1 (use delete button for that)
        if (newQty < 1)
        {
            SweetAlertService.ShowErrorAlert("Warning", "Warning Quantity cannot be less than 1. Use the delete button to remove the item.");

            return;
        }

        // Prevention: Optional check against max stock if you have that info
        if (cartItem.Product != null && newQty > cartItem.Product.Qty)
        {
            // Show error toast if you want
            return;
        }

        // Call Service (Use the same ID 5 or 1 that you are using)
        await CartService.UpdateQuantityAsync(1, cartItem.ProductId, newQty);

        // Refresh list locally to feel faster
        cartItem.Quantity = newQty;
    }

    private async Task DeleteCartItem(CartItem cartItem)
    {
        var confirmed = await SweetAlertService.ConfirmAsync(
            "Are you sure you want to remove this item from the cart?",
            $"You won't able to revert this! Deleting: {cartItem.Product!.Name}");
        if (confirmed)
        {
            // Call Service (Use the same ID 5 or 1 that you are using)
            await CartService.DeleteItemAsync(1, cartItem.ProductId);

            SweetAlertService.ShowSuccessToast($"Item: {cartItem.Product.Name} removed from cart successfully!");

            // Refresh cart list
            await LoadCart();
        }
        else
        {
            await LoadCart();
        }
    }

    private void GoToCheckout()
    {
        CloseModal(); // Cierra el carrito

        Navigation.NavigateTo("/checkout"); // Va a la nueva página
    }
}