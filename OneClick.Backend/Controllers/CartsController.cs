using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneClick.Backend.Repositories;
using OneClick.Shared.Entities;

namespace OneClick.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,Customer")] // We require authentication for all endpoints in this controller, as cart operations should be tied to a user account
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
            // In a real application, you would typically get the user ID from the authenticated user's claims rather than passing it as a parameter
            var result = await _cartRepository.GetCartItemsAsync(userId);

            // If the cart is empty, we can return an empty list or a 404 depending on your design choice. Here we choose to return an empty list.
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
            // Similar to GetCartItemsAsync, we would typically get the user ID from the authenticated user's claims in a real application
            var result = await _cartRepository.GetCartCountAsync(userId);

            //if (result == 0)
            //{
            //    return NotFound($"CartItem with ID: {userId} not found");
            //}

            // We return the count of items in the cart, which could be zero if the cart is empty
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

            // In a real application, you would typically get the user ID from the authenticated user's claims rather than passing it in the CartItem object
            var result = await _cartRepository.AddToCartAsync(cartItem);
            // We return the created cart item, which may include an assigned ID and any other properties set by the repository
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error Add: {ex.Message}");
        }
    }

    // We use a PUT method for updating the quantity of a cart item, as it is an idempotent operation that updates an existing resource
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