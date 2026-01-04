using OneClick.Shared.Entities;

namespace OneClick.Frontend.Services;

public interface ICartService
{
    // Event to notify components (like the Navbar badge) when the cart updates
    event Action OnChange;

    // Adds an item to the cart via API
    Task AddToCartAsync(CartItem cartItem);

    // Retrieves the list of items
    Task<List<CartItem>> GetCartItemsAsync(int userId);

    // Gets the total count of items
    Task<int> GetCartCountAsync(int userId);

    Task DeleteItemAsync(int userId, int productId);

    Task UpdateQuantityAsync(int userId, int productId, int newQuantity);
}