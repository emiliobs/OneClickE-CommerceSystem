using OneClick.Shared.Entities;
using System.Net.Http.Json;

namespace OneClick.Frontend.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    public async Task<int> PlaceOrderAsync(Order order)
    {
        try
        {
            // Send the order object to the backend OrdersController
            var response = await _httpClient.PostAsJsonAsync("api/Orders", order);

            if (response.IsSuccessStatusCode)
            {
                // If successful, the backend returns the ID of the new order
                return await response.Content.ReadFromJsonAsync<int>();
            }
            else
            {
                // If it fails, read the error message from the server
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error placing order: {errorMessage}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrderService] Exception in PlaceOrderAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        try
        {
            // Get the order details by ID
            var response = await _httpClient.GetAsync($"api/Orders/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Order>();
            }
            else
            {
                throw new Exception($"Error getting order details. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrderService] Exception in GetOrderByIdAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetOrdersByUsersIdAsync(int userId)
    {
        try
        {
            // Call the new API endpoint we just created in the controller to get orders by user ID
            var response = await _httpClient.GetAsync($"api/orders/user/{userId}");

            if (response.IsSuccessStatusCode)
            {
                // If the call is successful, we read the list of orders from the response
                var orders = await response.Content.ReadFromJsonAsync<IEnumerable<Order>>();

                // If the orders variable is null (which can happen if the user has no orders), we return
                // an empty list instead to avoid null reference issues in the UI
                return orders ?? new List<Order>(); // Return empty list if null
            }
            else
            {
                throw new Exception($"Failed to fetch order history. Status: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Order Services. Exception in GetOrdersByUserIdAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        try
        {
            // Calling the secure admin endpoint we created in the Controller
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<Order>>("api/orders/GetAllOrders");

            // If the result is null, we return an empty list to avoid null reference issues in the UI
            return result ?? new List<Order>(); // Return empty list if null
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Order services error fetching all orders: {ex.Message}");

            // In case of error, we return an empty list to avoid breaking the UI, but we log the error for debugging
            return new List<Order>();
        }
    }

    public async Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus)
    {
        try
        {
            // Call the backend API to update the order status. We send the new status in the body of the request.
            var response = await _httpClient.PutAsJsonAsync($"api/orders/update-status/{orderId}", newStatus);

            // Return true if the update was successful, false otherwise
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Order service error updating order status: {ex.Message}");
            return false;
        }
    }
}