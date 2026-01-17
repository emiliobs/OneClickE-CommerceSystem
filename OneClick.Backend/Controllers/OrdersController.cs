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

    [HttpPost]
    public async Task<ActionResult<int>> CreateOrderAsync([FromBody] Order order)
    {
        if (order is null || !ModelState.IsValid)
        {
            return BadRequest("Invalid order data.");
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
            return StatusCode(500, $"Error creating order: {ex.Message}");
        }
    }
}