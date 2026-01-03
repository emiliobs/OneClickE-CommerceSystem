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
}