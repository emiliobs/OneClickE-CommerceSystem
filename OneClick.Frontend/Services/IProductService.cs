using OneClick.Shared.Entities;

namespace OneClick.Frontend.Services;

public interface IProductService
{
    // Retrieves the full list of products from the API.

    Task<List<Product>> GetProductsAsync();

    // Retrieves a single product by its ID.

    Task<Product?> GetProductByIdAsync(int id);

    // Sends a POST request to create a new product.

    Task<Product?> CreateProductAsync(Product product);

    // Sends a PUT request to update an existing product.

    Task<bool> UpdateProductAsync(Product product);

    // Sends a DELETE request to remove a product.

    Task<bool> DeleteProductAsync(int id);
}