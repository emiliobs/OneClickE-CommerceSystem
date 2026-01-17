using OneClick.Shared.Entities;

namespace OneClick.Frontend.Services;

public interface IOrderService
{
    // Sends the order to the backend to be created
    Task<int> PlaceOrderAsync(Order order);

    // Retrieves an order by its ID (useful for confirmation page)
    Task<Order> GetOrderByIdAsync(int id);
}