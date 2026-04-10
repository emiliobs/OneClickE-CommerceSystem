using Microsoft.AspNetCore.Components;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;
using System.Net.Http.Json;

namespace OneClick.Frontend.Pages.MyOrders;

public partial class AdminDashboard
{
    // Services
    [Inject]
    public HttpClient http { get; set; } = default!;

    [Inject]
    public IOrderService OrderService { get; set; } = default!;

    [Inject]
    public IProductService ProductService { get; set; } = default!;

    [Inject]
    public AlertService SweetAlert { get; set; } = default!;

    // Metrics for the KPI Cards
    private decimal totalRevenue;

    private int pendingOrders;
    private int itemsSold;
    private List<Product> lowStockItems = new List<Product>();
    private bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        await LoadDashboardStats();
    }

    private async Task LoadDashboardStats()
    {
        try
        {
            // Simulate loading time
            isLoading = true;

            // Fetch all orders to calculate metrics
            var orders = await OrderService.GetAllOrdersAsync();

            // Calculate Total Revenue
            var ordersList = orders.ToList();

            if (ordersList.Any())
            {
                // Calculate Total Revenue by summing the TotalAmount of all non-cancelled orders
                totalRevenue = ordersList.Where(o => o.OrderStatus != "Cancelled").Sum(o => o.TotalAmount);

                // Calculate Total Items Sold by summing the quantity of all items in non-cancelled orders
                pendingOrders = ordersList.Count(o => o.OrderStatus == "Pending");

                // Calculate Total Items Sold by summing the quantity of all items in non-cancelled orders
                itemsSold = ordersList.SelectMany(o => o.OrderItems).Sum(oi => oi.Quantity);
            }

            // Fetch low stock items (e.g., products with stock less than 5)
            var stockResponse = await http.GetFromJsonAsync<List<Product>>("api/products/low-stock/5");

            // Update the low stock items list
            if (stockResponse != null)
            {
                lowStockItems = stockResponse;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Dashboard Error: {ex.Message}");
            await SweetAlert.ShowErrorAlert("Error", "Could not synchronize dashboard metics.");
        }
        finally
        {
            // End loading state
            isLoading = false;
        }
    }

    private async Task HandleRestock(Product product)
    {
        try
        {
            // For demonstration, we will just add a fixed quantity to the existing stock.
            var quantityToAdd = 10; // For example, restock by adding 10 units

            // Call the service to update the product stock
            var success = await ProductService.RestockProductAsync(product.Id, quantityToAdd);

            // If the restock operation was successful, update the dashboard stats
            if (success)
            {
                // Refresh the dashboard stats to reflect the updated stock levels
                await SweetAlert.ShowSuccessToast($"Success Restocked {product.Name} by {quantityToAdd} units.");

                // Reload the dashboard stats to reflect the updated stock levels
                await LoadDashboardStats();
            }
            else
            {
                // If the restock operation failed, show an error alert
                await SweetAlert.ShowErrorAlert("Error", $"Failed to restock {product.Name}.");
            }
        }
        catch (Exception ex)
        {
            // Log the error for debugging purposes
            await SweetAlert.ShowErrorAlert("Error", $"Restock updating: {ex.Message}");
        }
    }
}