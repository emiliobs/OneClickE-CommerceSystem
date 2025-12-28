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

    public async Task<Product> UpdateProductAsync(Product product)
    {
        var existProduct = await _context.Products.FindAsync(product.Id);
        if (existProduct is null)
        {
            return null!;
        }

        // Verify category exsit
        var categoryExist = await _context.Categories.AnyAsync(c => c.Id == product.CategoryId);
        if (!categoryExist)
        {
            throw new InvalidOperationException($"Category with ID {product.CategoryId} does not exist.");
        }

        existProduct.Name = product.Name;
        existProduct.Description = product.Description;
        existProduct.ImageURL = product.ImageURL;
        existProduct.Price = product.Price;
        existProduct.Qty = product.Qty;
        existProduct.CategoryId = product.CategoryId;

        await _context.SaveChangesAsync();

        return existProduct;
    }

    public async Task<Product> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
        {
            return null!;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return product;
    }
}