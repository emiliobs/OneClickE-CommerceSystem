using Microsoft.AspNetCore.Components;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;

namespace OneClick.Frontend.Pages.MyOrders;

public partial class MyOrders
{
    // Inject the service to fetch the data
    [Inject]
    public IOrderService OrderService { get; set; } = null!;

    [Inject]
    public AlertService SweetAlertService { get; set; } = default!;

    // State variables for the UI
    protected IEnumerable<Order>? orders;

    protected bool isLoading = true;

    // Harcode user ID for the current simulation
    private int currentUserID = 1;

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
            isLoading = true;

            // Fetch the orders from the backend API
            orders = await OrderService.GetOrdersByUsersIdAsync(currentUserID);
        }
        catch (Exception ex)
        {
            await SweetAlertService.ShowErrorAlert("My Orders Error loading order history: ", $"{ex.Message}");

            // If it fails, Initialize as an empty list to avoid null reference error in the HTML
            orders = new List<Order>();
        }
        finally
        {
            // Always stop laoding spinner, regardless of success or failure
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