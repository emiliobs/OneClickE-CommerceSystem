using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;
using System.Security.Claims;

namespace OneClick.Frontend.Pages.Checkout;

public partial class Checkout : ComponentBase
{
    [Inject] public IOrderService OrderService { get; set; } = null!;
    [Inject] public NavigationManager Navigation { get; set; } = null!;
    [Inject] public AlertService AlertService { get; set; } = null!;
    [Inject] public ICartService CartService { get; set; } = null!;

    // NEW: Inject the Authentication State Provider to read the JWT Token safely
    [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    protected Order order = new Order();
    protected bool isProcessing = false;
    private decimal cartTotal = 0;
    private int totalItems = 0;
    private bool isLoadingSummary = false;

    // We start at 0, and let the Token give us the real ID
    private int currentUserId = 0;

    protected override async Task OnInitializedAsync()
    {
        isLoadingSummary = true;

        // 1. Get the current authenticated user's state
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is not null && user.Identity.IsAuthenticated)
        {
            // 2. Extract the ID safely (checking the most common claim types)
            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst("id") ?? user.FindFirst("UserId");

            if (idClaim != null && int.TryParse(idClaim.Value, out int parsedId))
            {
                // 3. Assign the REAL user ID to the order
                currentUserId = parsedId;
                order.UserId = currentUserId;

                await LoadSummaryAsync();
            }
            else
            {
                await AlertService.ShowErrorAlert("Auth Error", "Could not identify your user session.");
                Navigation.NavigateTo("/login");
            }
        }
        else
        {
            // If they are not logged in, they shouldn't be in the checkout!
            Navigation.NavigateTo("/login");
        }
    }

    private async Task LoadSummaryAsync()
    {
        try
        {
            var cartItems = await CartService.GetCartItemsAsync(currentUserId);

            if (cartItems != null && cartItems.Any())
            {
                totalItems = cartItems.Sum(item => item.Quantity);
                cartTotal = cartItems.Sum(item => item.Product!.Price * item.Quantity);
            }
        }
        catch (Exception ex)
        {
            await AlertService.ShowErrorAlert("[Checkout] Error loading cart summary: ", ex.Message);
        }
        finally
        {
            isLoadingSummary = false;
        }
    }

    protected async Task ProcessCheckout()
    {
        if (currentUserId == 0) return; // Extra security check

        isProcessing = true;

        try
        {
            // IMPORTANT: Check your IOrderService interface. If you named the method CreateOrderAsync,
            // make sure to use CreateOrderAsync here instead of PlaceOrderAsync!
            int newOrderId = await OrderService.PlaceOrderAsync(order);

            await AlertService.ShowSuccessToast($"Order #{newOrderId} placed successfully!");
            Navigation.NavigateTo("/");
        }
        catch (Exception ex)
        {
            await AlertService.ShowErrorAlert("Order Failed", ex.Message);
        }
        finally
        {
            isProcessing = false;
        }
    }
}