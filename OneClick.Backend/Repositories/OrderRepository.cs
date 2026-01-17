using Microsoft.EntityFrameworkCore;
using OneClick.Backend.Data;
using OneClick.Shared.Entities;

namespace OneClick.Backend.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OneClickDbContext _context;

    public OrderRepository(OneClickDbContext context)
    {
        this._context = context;
    }

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        //  Get the user's cart items from the database
        // We include the Product to get the current price securely
        var cartItems = await _context.CartItems
                                      .Include(c => c.Product) // Include Product info to show names/images
                                      .Where(c => c.UserId == order.UserId)
                                      .ToListAsync();

        //  Validation: Cannot checkout with an empty cart
        if (cartItems == null || !cartItems.Any())
        {
            throw new InvalidOperationException("Cart is empty.");
        }

        //  Set server-side Order details
        order.OrderDate = DateTime.UtcNow;
        order.OrderStatus = "Pending";

        // Calculate the total amount securely on the server (never trust the client)
        order.TotalAmount = cartItems.Sum(c => c.Quantity * c.Product!.Price);

        //  Add the Order header to the database
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(); // Save here to generate the Order.Id

        //  Convert CartItems to OrderItems
        var orderItems = new List<OrderItem>();
        foreach (var item in cartItems)
        {
            orderItems.Add(new OrderItem
            {
                OrderId = order.Id, // Link to the new order
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Product!.Price // Lock in the price at the time of purchase
            });
        }

        _context.OrderItems.AddRange(orderItems);

        //  Clear the user's Shopping Cart
        _context.CartItems.RemoveRange(cartItems);

        //  Save all changes (OrderItems and Cart removal)
        await _context.SaveChangesAsync();

        return order;
    }
}