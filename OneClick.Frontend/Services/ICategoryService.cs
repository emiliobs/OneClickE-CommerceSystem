using OneClick.Shared.Entities;

namespace OneClick.Frontend.Services;

public interface ICategoryService
{
    // Get the full list of categories
    Task<List<Category>> GetAllCategoryAsync();

    // Get a single category by its ID (useful for editing)
    Task<Category?> GetByIdCategoryAsync(int id);

    // Create a new category and return the created object (with the new ID)
    Task<Category> AddCategoryAsync(Category category);

    // Update an existing category. Returns true if successful.
    Task<bool> UpdateCategoryAsync(Category category);

    // Delete a category by ID. Returns true if successful.

    Task<bool> DeleteCategoryAsync(int id);
}