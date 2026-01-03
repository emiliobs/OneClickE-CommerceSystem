using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneClick.Backend.Repositories;
using OneClick.Shared.Entities;

namespace OneClick.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly ICartRepository _cartRepository;

    public CartsController(ICartRepository cartRepository)
    {
        this._cartRepository = cartRepository;
    }

    [HttpGet("{userId:int}")]
    public async Task<ActionResult<List<CartItem>>> GetCartItemsAsync(int userId)
    {
        try
        {
            var result = await _cartRepository.GetCartItemsAsync(userId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error getting CartItems: {ex.Message}");
        }
    }

    [HttpGet("count/{userId:int}")]
    public async Task<ActionResult<int>> GetCartAcountAsync(int userId)
    {
        try
        {
            var result = await _cartRepository.GetCartCountAsync(userId);

            if (result == 0)
            {
                return NotFound($"CartItem with ID: {userId} not found");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Error GetCartCount: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<CartItem>> AddToCartSync(CartItem cartItem)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cartItem == null)
            {
                return BadRequest("Invalid cart item");
            }

            var result = await _cartRepository.AddToCartAsync(cartItem);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error Add: {ex.Message}");
        }
    }
}