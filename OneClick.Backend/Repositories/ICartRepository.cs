using OneClick.Shared.Entities;

namespace OneClick.Backend.Repositories;

public interface ICartRepository
{
    // Adds an item to the user's cart
    Task<CartItem> AddToCartAsync(CartItem cartItem);

    // Retrieves all items belonging to a specific user
    Task<List<CartItem>> GetCartItemsAsync(int userId);

    // Gets the total number of items (for the badge notification)
    Task<int> GetCartCountAsync(int userId);

    // Update the quantity of an existing item cart
    Task<bool> UpdateQuantityAsync(int userId, int productId, int newQuantity);

    // Removes an item completely from the cart
    Task<bool> DeleteItemAsync(int userId, int productId);
}