using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;
using System.Security.Claims;

namespace OneClick.Frontend.Pages.MyOrders;

public partial class MyOrders : ComponentBase
{
    [Inject] public IOrderService OrderService { get; set; } = null!;
    [Inject] public AlertService SweetAlertService { get; set; } = default!;
    [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
    [Inject] public IJSRuntime JSRuntime { get; set; } = default!;

    private List<Order> allOrders = new();
    private List<Order> filteredOrders = new();

    private bool isLoading = true;
    private string searchText = "";

    protected Order? selectedOrder = null;

    private int currentPage = 1;
    private int itemsPerPage = 7;

    public int TotalPages =>
        filteredOrders.Count == 0
            ? 1
            : (int)Math.Ceiling((double)filteredOrders.Count / itemsPerPage);

    public IEnumerable<Order> PaginatedOrders =>
        filteredOrders
            .Skip((currentPage - 1) * itemsPerPage)
            .Take(itemsPerPage);

    protected override async Task OnInitializedAsync()
    {
        await LoadOrderHistoryAsync();
    }

    private async Task LoadOrderHistoryAsync()
    {
        isLoading = true;
        StateHasChanged();

        try
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var idClaim =
                    user.FindFirst(ClaimTypes.NameIdentifier)
                    ?? user.FindFirst("id")
                    ?? user.FindFirst("UserId");

                if (idClaim != null && int.TryParse(idClaim.Value, out int currentUserID))
                {
                    var orders = await OrderService.GetOrdersByUsersIdAsync(currentUserID);
                    allOrders = orders.ToList();
                    filteredOrders = new List<Order>(allOrders);
                }
                else
                {
                    await SweetAlertService.ShowErrorAlert(
                        "Auth Error",
                        "Could not identify your user session.");
                }
            }
        }
        catch (Exception ex)
        {
            await SweetAlertService.ShowErrorAlert(
                "Error",
                $"Could not load order history: {ex.Message}");
        }
        finally
        {
            isLoading = false;
            StateHasChanged();
        }
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
            filteredOrders = allOrders
                .Where(o =>
                    o.Id.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    (o.OrderStatus != null &&
                     o.OrderStatus.Contains(searchText, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        currentPage = 1;
    }

    private void ChangePage(int newPage)
    {
        currentPage = newPage;
    }

    protected void ShowDetails(Order order)
    {
        selectedOrder = order;
    }

    protected void CloseDetails()
    {
        selectedOrder = null;
    }

    protected async Task PrintInvoiceAsync()
    {
        await JSRuntime.InvokeVoidAsync("window.print");
    }

    protected string GetStatusBadgeClass(string status) => status switch
    {
        "Pending" => "bg-primary",
        "Shipped" => "bg-warning text-dark",
        "Delivered" => "bg-success",
        "Cancelled" => "bg-danger",
        _ => "bg-secondary"
    };
}