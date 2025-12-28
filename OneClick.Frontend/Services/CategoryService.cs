using OneClick.Shared.Entities;
using System.Net.Http.Json;

namespace OneClick.Frontend.Services;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    // Get all categories

    public async Task<List<Category>> GetAllCategoryAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/categories");

            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
                return categories ?? new List<Category>();
            }

            Console.WriteLine($"Error getting categories: {response.StatusCode}");
            return new List<Category>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception getting categories: {ex.Message}");
            return new List<Category>();
        }
    }

    public async Task<Category?> GetByIdCategoryAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Category>($"api/categories/{id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception getting category {id}: {ex.Message}");
            return null!;
        }
    }

    public async Task<Category> AddCategoryAsync(Category category)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/categories", category);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Category>() ?? throw new Exception("Failed to parse response.");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error {response.StatusCode} : {error}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exeption aading category: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> UpdateCategoryAsync(int id, Category category)
    {
        try
        {
            // Ensure the the ID matches
            category.Id = id;

            var response = await _httpClient.PutAsJsonAsync($"api/categories/{id}", category);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error updating category: {response.StatusCode}: {error}");

                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception updating category: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error deleting category: {response.StatusCode} : {error}");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exeption deleting category: {ex.Message}");
            return false;
        }
    }
}