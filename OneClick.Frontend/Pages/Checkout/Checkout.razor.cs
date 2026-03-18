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

    // State variables
    protected Order order = new Order();

    protected bool isProcessing = false;

    // Hardcoded user for now (will change when we add Authentication)
    private int currentUserId = 1;

    protected override void OnInitialized()
    {
        // Assign the user ID to the order when the page loads
        order.UserId = currentUserId;
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