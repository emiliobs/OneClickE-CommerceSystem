using Microsoft.AspNetCore.Components;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;

namespace OneClick.Frontend.Pages.MyOrders;

public partial class AdminOrders
{
    // Inject the service to fetch the data
    [Inject]
    public IOrderService OrderService { get; set; } = null!;

    [Inject]
    public AlertService SweetAlertService { get; set; } = default!;

    // State variables for the UI
    protected List<Order> allOrders = new List<Order>();

    protected bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        await LoadAllOrdersAsync();
    }

    private async Task LoadAllOrdersAsync()
    {
        try
        {
            // Fetch all orders from the backend API
            isLoading = true;

            // Fetch the orders from the backend API
            var orders = await OrderService.GetAllOrdersAsync();
            // We convert the result to a list to ensure we have a concrete collection type for the UI
            allOrders = orders.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Admin orders error loading ordes: {ex.Message}");
            await SweetAlertService.ShowErrorAlert("Admin Orders Error: Could not load the global orders: ", $"{ex.Message}");
        }
        finally
        {
            // We set isLoading to false in the finally block to ensure it happens regardless of success or failure
            isLoading = false;
        }
    }

    private async Task ChangeStatusAsync(int orderId, string? newStatus)
    {
        // We validate the input before sending the request to avoid unnecessary API calls and to provide immediate
        // feedback to the user
        if (string.IsNullOrEmpty(newStatus))
        {
            return;
        }

        try
        {
            // Call the backend API to update the order status. We send the new status in the body of the request.
            var success = await OrderService.UpdateOrderStatusAsync(orderId, newStatus);

            if (success)
            {
                // If the update was successful, we update the status in our local list to reflect the change in the UI immediately
                await SweetAlertService.ShowSuccessToast($"Status Updated Order {orderId} status has been updated to {newStatus}.");
                await LoadAllOrdersAsync(); // Reload all orders to get the updated status
            }
            else
            {
                // If the update failed, we show an error message to the user
                await SweetAlertService.ShowErrorAlert($"Failed to update status for order {orderId}.", "Could not change the order status.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Admin order error updatinf status: {ex.Message}");
            await SweetAlertService.ShowErrorAlert($"Error updating order {orderId} status: ", $"{ex.Message}");
        }
    }

    protected string GetStatusBadgeClass(string status) => status switch
    {
        "Pending" => "badge bg-warning text-dark",
        "Shipped" => "badge bg-primary",
        "Delivered" => "badge bg-success",
        "Cancelled" => "badge bg-danger",
        _ => "badge bg-secondary"
    };
}