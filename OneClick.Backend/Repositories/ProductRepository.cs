using OneClick.Shared.Entities;

namespace OneClick.Backend.Repositories;

public class ProductRepository : IProductRepository
{
    public Task<Product> AddAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CategoryExistsAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Product product)
    {
        throw new NotImplementedException();
    }
}