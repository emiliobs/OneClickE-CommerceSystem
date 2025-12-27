using OneClick.Shared.Entities;
using System.Collections.Generic;

namespace OneClick.Backend.Repositories;

public interface ICategoryRepository
{
    Task<Category> GetByIdAsync(int id);

    Task<IEnumerable<Category>> GetAllAsync();

    Task<Category> AddAsync(Category category);

    Task UpdateAsync(Category category);

    Task DeleteAsync(int id);

    //Task<bool> ExistsAsync(int id);

    Task<bool> HasProductsAsync(int categoryId);
}