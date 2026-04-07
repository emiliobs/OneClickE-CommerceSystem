using OneClick.Shared.Entities;

namespace OneClick.Frontend.Services;

public interface IOrderService
{
    // Sends the order to the backend to be created
    Task<int> PlaceOrderAsync(Order order);

    // Retrieves an order by its ID (useful for confirmation page)
    Task<Order> GetOrderByIdAsync(int id);

    // Retrieves all orders for a specific user by their user ID (useful for order history page)
    Task<IEnumerable<Order>> GetOrdersByUsersIdAsync(int userId);

    Task<IEnumerable<Order>> GetAllOrdersAsync();

    Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus);
}