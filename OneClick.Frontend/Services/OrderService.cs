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
}