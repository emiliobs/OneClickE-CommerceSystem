using Microsoft.AspNetCore.Components;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;

namespace OneClick.Frontend.Pages.MyOrders;

public partial class AdminOrders
{
    // ================= SERVICES =================

    [Inject]
    public IOrderService OrderService { get; set; } = null!;

    [Inject]
    public AlertService SweetAlertService { get; set; } = default!;

    // ================= DATA =================

    // All orders from API
    protected List<Order> allOrders = new();

    // Filtered orders (VISIBLE TO USER)
    protected List<Order> filteredOrders = new();

    // ================= UI STATE =================

    protected bool isLoading = true;

    // Search text
    private string searchText = "";

    // ================= PAGINATION =================

    private int currentPage = 1;
    private int itemsPerPage = 7;

    public int TotalPages =>
        filteredOrders.Count == 0 ? 1 :
        (int)Math.Ceiling((double)filteredOrders.Count / itemsPerPage);

    public IEnumerable<Order> PaginatedOrders =>
        filteredOrders
            .Skip((currentPage - 1) * itemsPerPage)
            .Take(itemsPerPage);

    // ================= INITIALIZATION =================

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

            // IMPORTANT: initialize filtered list
            filteredOrders = new List<Order>(allOrders);
        }
        catch (Exception ex)
        {
            await SweetAlertService.ShowErrorAlert(
                "Admin Orders Error",
                ex.Message);
        }
        finally
        {
            isLoading = false;
        }
    }

    // ================= FILTER LOGIC =================

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
                || o.Id.ToString().Contains(searchText))
                .ToList();
        }

        // reset pagination after filtering
        currentPage = 1;
    }

    // ================= PAGINATION =================

    private void ChangePage(int newPage)
    {
        currentPage = newPage;
    }

    // ================= STATUS UPDATE =================

    private async Task ChangeStatusAsync(int orderId, string? newStatus)
    {
        if (string.IsNullOrEmpty(newStatus))
            return;

        try
        {
            var success = await OrderService.UpdateOrderStatusAsync(orderId, newStatus);

            if (success)
            {
                await SweetAlertService.ShowSuccessToast(
                    $"Order {orderId} updated to {newStatus}");

                await LoadAllOrdersAsync();
            }
            else
            {
                await SweetAlertService.ShowErrorAlert(
                    "Update Failed",
                    "Could not change order status.");
            }
        }
        catch (Exception ex)
        {
            await SweetAlertService.ShowErrorAlert(
                "System Error",
                ex.Message);
        }
    }

    // ================= BADGE COLORS =================

    protected string GetStatusBadgeClass(string status) => status switch
    {
        "Pending" => "badge bg-warning text-dark",
        "Shipped" => "badge bg-primary",
        "Delivered" => "badge bg-success",
        "Cancelled" => "badge bg-danger",
        _ => "badge bg-secondary"
    };
}