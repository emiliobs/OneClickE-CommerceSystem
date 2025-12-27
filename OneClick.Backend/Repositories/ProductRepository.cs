using OneClick.Shared.Entities;

namespace OneClick.Backend.Repositories;

public class ProductRepository : IProductRepository
{
    public Task<Product> AddProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CategoryExistsAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsProductAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetByIdProductAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }
}