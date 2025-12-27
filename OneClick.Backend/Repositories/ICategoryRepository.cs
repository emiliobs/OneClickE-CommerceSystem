using OneClick.Shared.Entities;
using System.Collections.Generic;

namespace OneClick.Backend.Repositories;

public interface ICategoryRepository
{
    Task<Category> GetByIdCategoryAsync(int id);

    Task<IEnumerable<Category>> GetAllCategoryAsync();

    Task<Category> AddCategoryAsync(Category category);

    Task UpdateCategoryAsync(Category category);

    Task DeleteCategoryAsync(int id);

    //Task<bool> ExistsAsync(int id);

    Task<bool> HasProductsAsync(int categoryId);
}