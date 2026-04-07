using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneClick.Backend.Repositories;
using OneClick.Shared.Entities;
using System.Security.Claims;

namespace OneClick.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
// We add the Authorize attribute to protect all endpoints in this controller,
// ensuring that only authenticated users can access them
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    // We inject the repository to access the data layer
    public OrdersController(IOrderRepository orderRepository)
    {
        this._orderRepository = orderRepository;
    }

    // GET api/Orders/5
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

    // GET api/Orders/user/5
    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserIdAsync(int userId)
    {
        try
        {
            // We can also get the user ID from the JWT token claims if needed, but we will use the route parameter for flexibility
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            //  We can also check the user's role if we want to restrict access to only their own orders, but for now we will allow
            //  admins to see all orders
            var currentUserRole = User.FindFirstValue(ClaimTypes.Role);

            if (currentUserRole != "Admin" && currentUserId != userId)
            {
                return Forbid("Access denied: You can only view your own order history.");
            }

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

    // POST api/Orders
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

    // GET api/Orders/GetAllOrders
    [HttpGet("GetAllOrders")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrdersAsync()
    {
        try
        {
            // Fetch the list of orders from the repository
            var orders = await _orderRepository.GetAllOrdersAsync();
            //  Return the list of orders to the client
            return Ok(orders);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Orders controller error getting all orders: {ex.Message}");
            return StatusCode(500, "Error retrieving all orders.");
        }
    }

    // PUT api/Orders/update-status/5
    [HttpPut("update-status/{orderId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateOrderStatusAsync(int orderId, [FromBody] string newStatus)
    {
        try
        {
            // Validate the new status (you can expand this with more specific rules if needed)
            if (string.IsNullOrWhiteSpace(newStatus))
            {
                return BadRequest("New status is required.");
            }

            // We delegate the logic to the repository
            var result = await _orderRepository.UpdateOrderStatusAsync(orderId, newStatus);

            if (!result)
            {
                return NotFound($"Order with ID {orderId} not found.");
            }

            return Ok(new { Message = "Order status update succeessfully." });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Orders controller error updating status: {ex.Message}");
            return StatusCode(500, "Error updating order status.");
        }
    }
}