using OneClick.Shared.Entities;
using System.Net.Http.Json;

namespace OneClick.Frontend.Services;

public class CartService : ICartService
{
    private readonly HttpClient _httpClient;
    private readonly SweetAlertService _sweetAlertService;

    public CartService(HttpClient httpClient, SweetAlertService sweetAlertService)
    {
        this._httpClient = httpClient;
        this._sweetAlertService = sweetAlertService;
    }

    //THe event that components subcribes to
    public event Action OnChange;

    public async Task AddToCartAsync(CartItem cartItem)
    {
        try
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
                // Log error if API fails (e.g., BadRequest)
                var error = await response.Content.ReadAsStringAsync();
                _sweetAlertService.ShowErrorAlert("Error", $"Error adding to cart: {error}");
            }
        }
        catch (Exception ex)
        {
            _sweetAlertService.ShowErrorAlert("Error", $"Http Request Error: {ex.Message}");
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
            _sweetAlertService.ShowErrorAlert("Error", $"Error fetching cart items: {ex.Message}");
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
            else
            {
                // If Backend returns 404 (NotFound), it means count is 0 based on your logic
                return 0;
            }
        }
        catch (Exception ex)
        {
            _sweetAlertService.ShowErrorAlert("Error", $"Error fetching cart count: {ex.Message}");

            return 0;
        }
    }

    public async Task UpdateQuantityAsync(int userId, int productId, int newQuantity)
    {
        // Call the put endpint we created early
        var response = await _httpClient.PutAsync($"api/Carts/update-quantity/{userId}/{productId}/{newQuantity}", null);

        if (response.IsSuccessStatusCode)
        {
            // Notify the UI (Badge) to update
            OnChange?.Invoke();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            _sweetAlertService.ShowErrorAlert("Error", $"Error updating cart item quantity: {error}");
        }
    }

    public async Task DeleteItemAsync(int userId, int productId)
    {
        try
        {
            // Call the delete endpoint we created early
            var response = await _httpClient.DeleteAsync($"api/Carts/{userId}/{productId}");
            if (response.IsSuccessStatusCode)
            {
                // Notify the UI (Badge) to update
                OnChange?.Invoke();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                _sweetAlertService.ShowErrorAlert("Error", $"Error deleting cart item: {error}");
            }
        }
        catch (Exception ex)
        {
            _sweetAlertService.ShowErrorAlert("Error", $"Http Request Error: {ex.Message}");
        }
    }
}