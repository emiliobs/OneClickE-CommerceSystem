using Microsoft.EntityFrameworkCore;
using OneClick.Backend.Data;
using OneClick.Shared.Entities;

namespace OneClick.Backend.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly OneClickDbContext _context;

    public CategoryRepository(OneClickDbContext context)
    {
        this._context = context;
    }

    public async Task<Category> GetByIdAsync(int id) => await _context.Categories.FindAsync(id);

    public async Task<IEnumerable<Category>> GetAllAsync() => await _context.Categories.OrderBy(c => c.Name).ToListAsync();

    public async Task<Category> AddAsync(Category category)
    {
        // Check if category with the same name already exist
        var existCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == category.Name.ToLower());

        if (existCategory != null)
        {
            throw new InvalidOperationException($"A Category with name '{category.Name}' already exist.");
        }

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task UpdateAsync(Category category)
    {
        // Check if another category with  same name exist
        var duplicate = await _context.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == category.Name.ToLower()
        && c.Id != category.Id);

        if (duplicate != null)
        {
            throw new InvalidOperationException($"A Category with name '{category.Name}' already exists.");
        }

        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await GetByIdAsync(id);

        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }

    //public async Task<bool> ExistsAsync(int id)
    //{
    //    return await _context.Categories.AnyAsync(c => c.Id == id);
    //}

    public async Task<bool> HasProductsAsync(int categoryId)
    {
        return await _context.Products.AnyAsync(p => p.CategoryId == categoryId);
    }
}