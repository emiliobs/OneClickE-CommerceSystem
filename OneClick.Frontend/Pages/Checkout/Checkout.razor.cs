using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;

namespace OneClick.Frontend.Pages.Checkout;

public partial class Checkout
{
    // Inject the necessary services
    [Inject]
    public IOrderService OrderService { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    // Inject our custom AlertService
    [Inject]
    public AlertService AlertService { get; set; } = null!;

    [Inject]
    public ICartService CartService { get; set; } = null;

    // State variables
    protected Order order = new Order();

    protected bool isProcessing = false;

    // State varaibles for the Summary Panel
    private decimal cartTotal = 0;

    private int totalItems = 0;
    private bool isLoadingSummary = false;

    // Hardcoded user for now (will change when we add Authentication)
    private int currentUserId = 1;

    protected override async Task OnInitializedAsync()
    {
        // Assign the user ID to the order when the page loads
        order.UserId = currentUserId;

        await LoadSummaryAsync();
    }

    // Method to calclulate the total price ans item count for the summary panel
    private async Task LoadSummaryAsync()
    {
        try
        {
            var cartItems = await CartService.GetCartItemsAsync(currentUserId);

            if (cartItems != null && cartItems.Any())
            {
                // Sum the queanity of all items to get the total item count
                totalItems = cartItems.Sum(item => item.Quantity);

                // Sum the quantity price (Price * quantity for each item)
                cartTotal = cartItems.Sum(item => item.Product!.Price * item.Quantity);
            }
        }
        catch (Exception ex)
        {
            await AlertService.ShowErrorAlert("[Checkout]  Error loading cart summary: ", ex.Message);
        }
        finally
        {
            // Stop the loading indicator for the summary panel
            isLoadingSummary = false;
        }
    }

    // Handler for the form submission
    protected async Task ProcessCheckout()
    {
        // Show loading indicator on the button
        isProcessing = true;

        try
        {
            // Send the order to the backend
            int newOrderId = await OrderService.PlaceOrderAsync(order);

            // If successful, show a success toast notification
            await AlertService.ShowSuccessToast($"Order #{newOrderId} placed successfully!");

            // Redirect the user to the Home page
            Navigation.NavigateTo("/");
        }
        catch (Exception ex)
        {
            // If there is an error (e.g., empty cart), show an error alert
            await AlertService.ShowErrorAlert("Order Failed", ex.Message);
        }
        finally
        {
            // Stop the loading indicator
            isProcessing = false;
        }
    }
}