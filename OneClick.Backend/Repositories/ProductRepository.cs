using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OneClick.Backend.Data;
using OneClick.Shared.Entities;

namespace OneClick.Backend.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly OneClickDbContext _context;

    public ProductRepository(OneClickDbContext context)
    {
        this._context = context;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.Include(p => p.Category).OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByCategoryIdAsync(int categoryId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        // Verify category exist
        var categoryExist = await _context.Categories.AnyAsync(c => c.Id == product.CategoryId);

        if (!categoryExist)
        {
            throw new InvalidOperationException($"Category with ID {product.CategoryId} does not exist.");
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Reload with category for response
        await _context.Entry(product).Reference(p => p.Category).LoadAsync();

        return product;
    }

    public async Task UpdateProductAsync(Product product)
    {
        // Verify category exsit
        var categoryExist = await _context.Categories.AnyAsync(c => c.Id == product.CategoryId);
        if (!categoryExist)
        {
            throw new InvalidOperationException($"Category with ID {product.CategoryId} does not exist.");
        }

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var existProduct = await GetProductByIdAsync(id);
        if (existProduct is not null)
        {
            _context.Products.Remove(existProduct);
            await _context.SaveChangesAsync();
        }
    }
}