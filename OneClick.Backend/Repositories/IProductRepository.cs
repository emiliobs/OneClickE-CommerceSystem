using OneClick.Shared.Entities;

namespace OneClick.Backend.Repositories;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);

    Task<IEnumerable<Product>> GetAllProductsAsync();

    Task<IEnumerable<Product>> GetProductByCategoryIdAsync(int categoryId);

    Task<Product> AddProductAsync(Product product);

    Task<Product> UpdateProductAsync(Product product);

    Task<Product> DeleteProductAsync(int id);
}