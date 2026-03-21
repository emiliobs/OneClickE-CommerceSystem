using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneClick.Backend.Repositories;
using OneClick.Shared.Entities;

namespace OneClick.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IOrderRepository orderRepository)
    {
        this._orderRepository = orderRepository;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Order>> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id);

        if (order is null)
        {
            return NotFound($"Order with ID {id} not found.");
        }

        return Ok(order);
    }

    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserIdAsync(int userId)
    {
        try
        {
            // Fetch the list of orders from the repository based on the user ID
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Orders Controller. Error getting user orders: {ex.Message}");
            return StatusCode(500, "Error retrieving order history.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateOrderAsync([FromBody] Order order)
    {
        // Check if the model is valid manually to send a clean message
        if (!ModelState.IsValid)
        {
            // Extract all validation error message into a single string
            var errors = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

            return BadRequest($"Validation failed: {errors}");
        }

        try
        {
            // We delegate the logic to the repository
            var createdOrder = await _orderRepository.CreateOrderAsync(order);

            // Return the new Order ID so the frontend can show a confirmation
            return Ok(createdOrder.Id);
        }
        catch (Exception ex)
        {
            // Capture inner database exception if they exist for more detailed error messages
            var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

            // Log it in the backend console
            Console.WriteLine($"OrcerController Cirtical Error: {actualError}");

            return StatusCode(500, $"Error creating order: {actualError}");
        }
    }
}