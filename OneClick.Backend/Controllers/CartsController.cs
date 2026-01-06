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

    [HttpPut("update-quantity/{userId:int}/{productId:int}/{newQuantity:int}")]
    public async Task<ActionResult<bool>> UpdateteCartItemQuantityAsync(int userId, int productId, int newQuantity)
    {
        try
        {
            var result = await _cartRepository.UpdateQuantityAsync(userId, productId, newQuantity);

            if (!result)
            {
                return NotFound($"Cart item for User ID: {userId} and Product ID: {productId} not found");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error UpdateQuantity: {ex.Message}");
        }
    }

    [HttpDelete("{userId:int}/{productId:int}")]
    public async Task<ActionResult<bool>> DeleteCartItemAsync(int userId, int productId)
    {
        try
        {
            var result = await _cartRepository.DeleteItemAsync(userId, productId);

            if (!result)
            {
                return NotFound($"Cart item for User ID: {userId} and Product ID: {productId} not found");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error Delete: {ex.Message}");
        }
    }
}