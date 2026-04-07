using OneClick.Shared.Entities;

namespace OneClick.Backend.Repositories;

public interface IOrderRepository
{
    // Creates an order based on the user's current cart
    Task<Order> CreateOrderAsync(Order order);

    Task<Order?> GetOrderByIdAsync(int id);

    // Method to get all orders for a specific user by their user ID
    Task<IEnumerable<Order?>> GetOrdersByUserIdAsync(int userId);

    Task<IEnumerable<Order>> GetAllOrdersAsync();

    Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus);
}