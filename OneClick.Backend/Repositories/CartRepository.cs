using Microsoft.EntityFrameworkCore;
using OneClick.Backend.Data;
using OneClick.Shared.Entities;

namespace OneClick.Backend.Repositories;

public class CartRepository : ICartRepository
{
    private readonly OneClickDbContext _context;

    public CartRepository(OneClickDbContext context)
    {
        this._context = context;
    }

    public async Task<List<CartItem>> GetCartItemsAsync(int userId)
    {
        // Get the list and include Prodcut details (for the in=mage and name)
        return await _context.CartItems.AsNoTracking().Where(ci => ci.UserId == userId).Include(ci => ci.Product).ToListAsync();
    }

    public async Task<int> GetCartCountAsync(int userId)
    {
        // Calculate total quantity of all items in the cart
        var count = await _context.CartItems.Where(ci => ci.UserId == userId).SumAsync(ci => ci.Quantity);

        return count;
    }

    public async Task<CartItem> AddToCartAsync(CartItem cartItem)
    {
        // Check if the product is already in the cart for this user
        var sameItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId && ci.UserId == cartItem.UserId);

        if (sameItem == null)
        {
            // It's a new item, add it properly
            _context.CartItems.Add(cartItem);
            // Save changes to SQL Database
            await _context.SaveChangesAsync();

            return cartItem;
        }
        else
        {
            // It already exist, just increase quantety
            sameItem.Quantity += cartItem.Quantity;
            await _context.SaveChangesAsync();
            return cartItem;
        }
    }
}