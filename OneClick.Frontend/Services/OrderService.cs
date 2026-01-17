using OneClick.Shared.Entities;

namespace OneClick.Frontend.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    public Task<Order> GetOrderByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> PlaceOrderAsync(Order order)
    {
        throw new NotImplementedException();
    }
}