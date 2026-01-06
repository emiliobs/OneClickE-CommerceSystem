using OneClick.Shared.Entities;
using System.Net.Http.Json;

namespace OneClick.Frontend.Services;

public class CartService : ICartService
{
    private readonly HttpClient _httpClient;

    // Modified: We removed SweetAlertService from dependency injection.
    // Now the service is pure and only handles HTTP logic.
    public CartService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // The event that components subscribe to (to update the badge)
    public event Action? OnChange;

    public async Task AddToCartAsync(CartItem cartItem)
    {
        // Calls POST api/Carts
        var response = await _httpClient.PostAsJsonAsync("api/Carts", cartItem);

        if (response.IsSuccessStatusCode)
        {
            // Notify the UI (Badge) to update
            OnChange?.Invoke();
        }
        else
        {
            // If it fails, we throw an exception with the message from the API.
            // The Component (UI) will catch this and show the alert.
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }

    public async Task<List<CartItem>> GetCartItemsAsync(int userId)
    {
        try
        {
            // Calls GET api/Carts/{userId}
            var items = await _httpClient.GetFromJsonAsync<List<CartItem>>($"api/Carts/{userId}");
            return items ?? new List<CartItem>();
        }
        catch (Exception ex)
        {
            // On read operations, it is often safer to return an empty list
            // and log the error to the console rather than crashing the page.
            Console.WriteLine($"Error fetching cart items: {ex.Message}");
            return new List<CartItem>();
        }
    }

    public async Task<int> GetCartCountAsync(int userId)
    {
        try
        {
            // Calls GET api/Carts/count/{userId}
            var response = await _httpClient.GetAsync($"api/Carts/count/{userId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int>();
            }

            // If Backend returns 404 or error, assume count is 0
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching cart count: {ex.Message}");
            return 0;
        }
    }

    public async Task UpdateQuantityAsync(int userId, int productId, int newQuantity)
    {
        var response = await _httpClient.PutAsync($"api/Carts/update-quantity/{userId}/{productId}/{newQuantity}", null);

        if (response.IsSuccessStatusCode)
        {
            OnChange?.Invoke();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error updating quantity: {error}");
        }
    }

    public async Task DeleteItemAsync(int userId, int productId)
    {
        var response = await _httpClient.DeleteAsync($"api/Carts/{userId}/{productId}");

        if (response.IsSuccessStatusCode)
        {
            OnChange?.Invoke();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error deleting item: {error}");
        }
    }
}