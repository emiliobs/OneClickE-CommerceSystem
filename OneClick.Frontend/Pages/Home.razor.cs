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

    // List to store data from the API
    private List<Product> products = new List<Product>();

    private string? message;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            // Call the services to get the List of products
            products = await ProductService.GetProductsAsync();
        }
        catch (Exception ex)
        {
            SweetAlertService.ShowErrorAlert("Error loading products:", ex.Message);

            //Initialize empty List to avoid null reference errors in HTML
            products = new List<Product>();
        }
    }

    // 2. HANDLE THE EVENT (Lógica del Padre)
    private async Task HandleAddToCart(Product product)
    {
        SweetAlertService.ShowSuccessToast($"Clicked on: {product.Name}");
    }
}