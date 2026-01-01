using Microsoft.AspNetCore.Components;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;
using System.Threading.Tasks;

namespace OneClick.Frontend.Pages;

public partial class Home
{
    [Inject]
    public Services.SweetAlertService SweetAlertService { get; set; } = default!;

    [Inject]
    public IProductService ProductService { get; set; } = default!;

    [Inject]
    public ICategoryService CategoryService { get; set; } = default!;

    //UI State
    private bool isLoading = true;

    // Pagination Variables
    private int currentPage = 1;

    private int pageSize = 20; // Muestra 20 prodcuts por paginas
    private int totalPages = 0;

    // Filter Variables (Boud to the Component)
    private string searchText = "";

    private string selectedCategory = "All";
    private int selectedStatus = 0; // 0:All, 1:Available, 2:Offers, 3:New

    // Propiedades publicas que resetean la pgina a 1 cuando cambian

    // ----- Data -----
    // List to store data from the API
    private List<Product> products = new List<Product>();

    private List<Category> categories = new List<Category>();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            isLoading = true;

            // Call the services to get the List of products
            products = await ProductService.GetProductsAsync();
            categories = await CategoryService.GetAllCategoryAsync();
        }
        catch (Exception ex)
        {
            SweetAlertService.ShowErrorAlert("Error loading products:", ex.Message);

            //Initialize empty List to avoid null reference errors in HTML
            products = new List<Product>();
        }
        finally
        {
            isLoading = false;
        }
    }

    // LOgic to filter the list based on All criteria
    private List<Product> GetFilteredProducts()
    {
        var filtered = products.AsEnumerable();

        // Text search
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            filtered = filtered.Where(p => p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                                      ||
                                      p.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase)

            );
        }

        // Category Filter
        if (selectedCategory != "All")
        {
            filtered = filtered.Where(p => p.Category?.Name == selectedCategory);
        }

        // 3. Status Tabs Logic (Mapped to existing properties)
        switch (selectedStatus)
        {
            case 1: // Available
                filtered = filtered.Where(p => p.Qty > 0);
                break;

            case 2: // Offers (Placeholder logic: e.g., Price < 100 or specific ID), Since we don't have 'IsOffer', let's just show items under $500 as an example
                filtered = filtered.Where(p => p.Price < 55);
                break;

            case 3: // New (Placeholder logic: e.g., Last 5 items), Taking the last added items by ID usually implies newness
                filtered = filtered.OrderByDescending(p => p.Id).Take(5);
                break;
        }

        return filtered.ToList();
    }

    // HANDLE THE EVENT (Lógica del Padre)
    private async Task HandleAddToCart(Product product)
    {
        SweetAlertService.ShowSuccessToast($"Clicked on: {product.Name} To cart!");

        if (product.Qty > 0)
        {
            product.Qty--;
        }
    }
}