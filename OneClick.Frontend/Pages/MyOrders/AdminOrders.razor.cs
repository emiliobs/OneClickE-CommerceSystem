using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OneClick.Frontend.Services;
using OneClick.Shared.DTOs;
using OneClick.Shared.Entities;

namespace OneClick.Frontend.Pages.MyOrders;

public partial class AdminOrders
{
    [Inject] public IOrderService OrderService { get; set; } = null!;
    [Inject] public AlertService SweetAlertService { get; set; } = default!;
    [Inject] public IJSRuntime JSRuntime { get; set; } = default!;

    protected List<Order> allOrders = new();
    protected List<Order> filteredOrders = new();
    protected bool isLoading = true;
    private string searchText = "";
    protected Order? selectedOrder = null;

    // Pagination properties
    private int currentPage = 1;

    private int itemsPerPage = 6;

    public int TotalPages => filteredOrders.Count == 0 ? 1 : (int)Math.Ceiling((double)filteredOrders.Count / itemsPerPage);
    public IEnumerable<Order> PaginatedOrders => filteredOrders.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage);

    protected override async Task OnInitializedAsync()
    {
        await LoadAllOrdersAsync();
    }

    private async Task LoadAllOrdersAsync()
    {
        try
        {
            isLoading = true;
            var orders = await OrderService.GetAllOrdersAsync();
            allOrders = orders.ToList();
            filteredOrders = new List<Order>(allOrders);
        }
        catch (Exception ex)
        {
            await SweetAlertService.ShowErrorAlert("Admin Orders Error", ex.Message);
        }
        finally { isLoading = false; }
    }

    private void FilterOrders(ChangeEventArgs e)
    {
        searchText = e.Value?.ToString() ?? "";
        if (string.IsNullOrWhiteSpace(searchText))
        {
            filteredOrders = new List<Order>(allOrders);
        }
        else
        {
            filteredOrders = allOrders.Where(o =>
                (o.User?.FirstName ?? "").Contains(searchText, StringComparison.OrdinalIgnoreCase)
                || (o.User?.LastName ?? "").Contains(searchText, StringComparison.OrdinalIgnoreCase)
                || o.OrderStatus.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                || o.Id.ToString().Contains(searchText)).ToList();
        }
        currentPage = 1;
    }

    private void ChangePage(int newPage) => currentPage = newPage;

    protected async Task ShowDetails(int orderId)
    {
        try
        {
            // Fetch fresh data from backend including User and Items
            selectedOrder = await OrderService.GetOrderByIdAsync(orderId);
        }
        catch (Exception ex)
        {
            await SweetAlertService.ShowErrorAlert("Error", "Could not load order details.");
        }
    }

    protected void CloseDetails() => selectedOrder = null;

    protected async Task PrintInvoiceAsync() => await JSRuntime.InvokeVoidAsync("print");

    private async Task ChangeStatusAsync(int orderId, string? newStatus)
    {
        if (string.IsNullOrEmpty(newStatus)) return;
        try
        {
            var success = await OrderService.UpdateOrderStatusAsync(orderId, newStatus);
            if (success)
            {
                await SweetAlertService.ShowSuccessToast($"Order {orderId} updated to {newStatus}");
                await LoadAllOrdersAsync();
            }
        }
        catch (Exception ex)
        {
            await SweetAlertService.ShowErrorAlert("System Error", ex.Message);
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