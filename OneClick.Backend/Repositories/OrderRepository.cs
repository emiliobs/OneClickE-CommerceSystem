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

    public async Task<Order> CreateOrderAsync(Order order)
    {
        // Get the user's cart items from the database, we include the Product to get the cirrent price securely.
        var cartItems = await _context.CartItems.Include(c => c.Product).Where(c => c.UserId == order.UserId).ToListAsync();

        // Validation: Cannot checkout with the empty cart:
        if (cartItems is null || !cartItems.Any())
        {
            throw new InvalidOperationException("CArt is empty.");
        }

        // Set server-side Order details
        order.OrderDate = DateTime.UtcNow;
        order.OrderStatus = "Pending";

        // CAlculate the total amount securely on the server (never trust the client)
        order.TotalAmount = cartItems.Sum(ci => ci.Quantity * ci.Product!.Price);

        // Add the order header to the database
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(); // Save here to generate the Order Order.Id

        // Convert CartItems to OrdersItems
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

        // Clear the user's Shooping Cart
        _context.CartItems.RemoveRange(cartItems);

        // Save all change (OrderItems and Cart removal)
        await _context.SaveChangesAsync();

        return order;
    }
}