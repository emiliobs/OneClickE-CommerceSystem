using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;
using System.Security.Claims;

namespace OneClick.Frontend.Pages.Products;

public partial class ShoppingCart : ComponentBase
{
    // Inject required services for cart logic and UI alerts
    [Inject] public ICartService CartService { get; set; } = default!;

    [Inject] public AlertService SweetAlertService { get; set; } = default!;
    [Inject] public NavigationManager Navigation { get; set; } = default!;

    // NEW: Inject the Authentication Provider to secure the cart
    [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    // Parameters to control the modal visibility from the parent component
    [Parameter] public bool IsOpen { get; set; }

    [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }

    // State variables
    private List<CartItem> cartItems = new List<CartItem>();

    private bool isLoading = true;

    // NEW: Dynamic User ID
    private int currentUserId = 0;

    protected override async Task OnParametersSetAsync()
    {
        // Only load the cart from the database if the modal is being opened
        if (IsOpen)
        {
            await LoadCart();
        }
    }

    // NEW: Helper method to retrieve the logged-in user's ID securely
    private async Task<int> GetCurrentUserIdAsync()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is not null && user.Identity.IsAuthenticated)
        {
            // Safely parse the user ID from the claims
            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst("id") ?? user.FindFirst("UserId");
            if (idClaim != null && int.TryParse(idClaim.Value, out int parsedId))
            {
                return parsedId;
            }
        }
        return 0; // Return 0 if the user is anonymous
    }

    private async Task LoadCart()
    {
        isLoading = true;
        try
        {
            // 1. Get the real user ID
            currentUserId = await GetCurrentUserIdAsync();

            // 2. Fetch the cart ONLY for the authenticated user (no more ID 1!)
            if (currentUserId > 0)
            {
                cartItems = await CartService.GetCartItemsAsync(currentUserId);
            }
            else
            {
                // Clear the cart view if nobody is logged in
                cartItems = new List<CartItem>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ShoppingCart] Error loading cart: {ex.Message}");
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
        // Close the cart and notify the parent component
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
            await SweetAlertService.ShowErrorAlert("Warning", "Quantity cannot be less than 1. Use the delete button to remove the item.");
            return;
        }

        // Prevention: Optional check against max stock from the product entity
        if (cartItem.Product != null && newQty > cartItem.Product.Qty)
        {
            await SweetAlertService.ShowErrorAlert("Out of Stock", "Not enough items in stock.");
            return;
        }

        // Fetch the user ID securely before updating the database
        currentUserId = await GetCurrentUserIdAsync();
        if (currentUserId > 0)
        {
            // Call the backend service using the dynamic User ID
            await CartService.UpdateQuantityAsync(currentUserId, cartItem.ProductId, newQty);

            // Refresh the list locally to make the UI feel faster
            cartItem.Quantity = newQty;
        }
    }

    private async Task DeleteCartItem(CartItem cartItem)
    {
        // Ask for user confirmation before deleting
        var confirmed = await SweetAlertService.ConfirmAsync(
            "Are you sure you want to remove this item from the cart?",
            $"You won't be able to revert this! Deleting: {cartItem.Product!.Name}");

        if (confirmed)
        {
            currentUserId = await GetCurrentUserIdAsync();
            if (currentUserId > 0)
            {
                // Call the backend service using the dynamic User ID
                await CartService.DeleteItemAsync(currentUserId, cartItem.ProductId);

                await SweetAlertService.ShowSuccessToast($"Item: {cartItem.Product!.Name} removed from cart successfully!");

                // Refresh the cart list to show the new state
                await LoadCart();
            }
        }
        else
        {
            // Reload just to ensure the state is clean if cancelled
            await LoadCart();
        }
    }

    private async Task GoToCheckout()
    {
        // 1. Close the cart modal so it doesn't stay open during navigation
        await CloseModal();

        // 2. Navigate to the Checkout page
        Navigation.NavigateTo("/checkout");
    }
}