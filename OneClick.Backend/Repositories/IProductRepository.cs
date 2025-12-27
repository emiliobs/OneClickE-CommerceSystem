using OneClick.Shared.Entities;

namespace OneClick.Backend.Repositories;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);

    Task<IEnumerable<Product>> GetAllProductsAsync();

    Task<IEnumerable<Product>> GetByIdProductAsync(int categoryId);

    Task<Product> AddProductAsync(Product product);

    Task UpdateProductAsync(Product product);

    Task DeleteProductAsync(int id);

    Task<bool> ExistsProductAsync(int id);

    Task<bool> CategoryExistsAsync(int categoryId);
}