using OneClick.Shared.Entities;
using System.Net.Http.Json;

namespace OneClick.Frontend.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    // Dependency Injection of the HttpClient configured in Program.cs
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        // Get data from API. If null, return an empty list to avoid crashes.
        var response = await _httpClient.GetFromJsonAsync<List<Product>>("api/products");
        return response ?? new List<Product>();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Product>($"api/products/{id}");
    }

    public async Task<Product?> CreateProductAsync(Product product)
    {
        var response = await _httpClient.PostAsJsonAsync("api/products", product);

        if (response.IsSuccessStatusCode)
        {
            // Return the created object (useful if backend assigns the ID)
            return await response.Content.ReadFromJsonAsync<Product>();
        }
        return null;
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        // Send PUT request
        var response = await _httpClient.PutAsJsonAsync($"api/products/{product.Id}", product);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        // Send DELETE request
        var response = await _httpClient.DeleteAsync($"api/products/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<string> UploadImageAsync(MultipartFormDataContent content)
    {
        var response = await _httpClient.PostAsync($"api/products/UploadImage", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }

        var result = await response.Content.ReadFromJsonAsync<UploadResult>();
        return result!.Url;
    }
}