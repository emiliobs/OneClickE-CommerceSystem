using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;
using System.Security;
using System.Security.Claims;

namespace OneClick.Frontend.Pages.MyOrders;

public partial class MyOrders
{
    // Inject the service to fetch the data
    [Inject]
    public IOrderService OrderService { get; set; } = null!;

    // Inject the SweetAlert service for user-friendly error messages
    [Inject]
    public AlertService SweetAlertService { get; set; } = default!;

    // Inject the AuthenticationStateProvider to get information about the current user (if needed in the future)
    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    // State variables for the UI
    protected IEnumerable<Order>? orders;

    protected bool isLoading = true;

    // Harcode user ID for the current simulation
    //private int currentUserID = 1;

    // Holds the order that the user wants to see in detail
    protected Order? selectedOrder;

    // This method runs automatically when the pasge loads
    protected override async Task OnInitializedAsync()
    {
        await LoadOrderHistoryAsync();
    }

    // Encapsulated method to load the data safely
    private async Task LoadOrderHistoryAsync()
    {
        try
        {
            // Set loading state to true at the beginning of the operation
            isLoading = true;

            // Get the current user's authentication state
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            // Extract the user from the authentication state
            var user = authState.User;

            // Check if the user is authenticated before trying to access their ID
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                // FIX: Look for the User ID in multiple common claim types
                // Sometimes it's saved as "NameIdentifier", sometimes simply as "id"
                var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)
                           ?? user.FindFirst("id")
                           ?? user.FindFirst("UserId");

                // DEBUGGING: Log the claim we found (if any)
                if (idClaim != null && int.TryParse(idClaim.Value, out int currentUserID))
                {
                    Console.WriteLine($"[MyOrders] Successfully found User ID: {currentUserID}");

                    // Now we can safely call the service to get the orders for this user
                    orders = await OrderService.GetOrdersByUsersIdAsync(currentUserID);

                    // DEBUGGING: Log the number of orders we received
                    if (!orders.Any())
                    {
                        Console.WriteLine($"[MyOrders] The API returned an empty list for User ID: {currentUserID}");
                    }
                }
                else
                {
                    // DEBUGGING: If we hit this, it means we can't find the ID in the token
                    Console.WriteLine("[MyOrders ERROR] Could not find the ID claim in the token. Available claims:");

                    // Log all claims for debugging purposes
                    foreach (var claim in user.Claims)
                    {
                        Console.WriteLine($"  - {claim.Type}: {claim.Value}");
                    }

                    await SweetAlertService.ShowErrorAlert("Auth Error", "Could not identify your User ID from the session.");
                    orders = new List<Order>();
                }
            }
        }
        catch (Exception ex)
        {
            await SweetAlertService.ShowErrorAlert("Error", $"Could not load order history: {ex.Message}");
            orders = new List<Order>();
        }
        finally
        {
            // Set loading state to false at the end of the operation, regardless of success or failure
            isLoading = false;
        }
    }

    // Method to open the modal
    protected void ShowDetails(Order order)
    {
        selectedOrder = order;
    }

    //  Methos to close the modal
    protected void CloseDetails()
    {
        selectedOrder = null;
    }
}