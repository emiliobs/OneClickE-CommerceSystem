using OneClick.Shared.Entities;

namespace OneClick.Backend.Repositories;

public interface IOrderRepository
{
    // Creates an order based on the user's current cart
    Task<Order> CreateOrderAsync(Order order);
}