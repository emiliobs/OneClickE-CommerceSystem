using OneClick.Shared.Entities;

namespace OneClick.Backend.Repositories;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(int id);

    Task<IEnumerable<Product>> GetAllAsync();

    Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);

    Task<Product> AddAsync(Product product);

    Task UpdateAsync(Product product);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);

    Task<bool> CategoryExistsAsync(int categoryId);
}