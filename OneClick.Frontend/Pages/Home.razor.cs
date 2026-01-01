using Microsoft.AspNetCore.Components;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;
using System.Security;
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

    private int pageSize = 12; // Muestra 20 prodcuts por paginas
    private int totalPages = 0;

    // Filter Variables (Boud to the Component),Usamos campos privados para guardar el valor
    private string _searchText = "";

    private string _selectedCategory = "All";
    private int _selectedStatus = 0; // 0:All, 1:Available, 2:Offers, 3:New

    // Propiedades publicas que resetean la pgina a 1 cuando cambian
    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            currentPage = 1; // Reset a pagina 1 al buscar
        }
    }

    public string SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            _selectedCategory = value;
            currentPage = 1; // Reset a pagina 1 al cambiar categoria
        }
    }

    public int SelectedStatus
    {
        get => _selectedStatus;
        set
        {
            _selectedStatus = value;
            currentPage = 1; // Reset a pagina 1 al cambiar estado
        }
    }

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
    private List<Product> GetFilteredAndPAginateProducts()
    {
        // Aplicar Filtros
        var filtered = products.AsEnumerable();

        // Text search
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filtered = filtered.Where(p => p.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                                      ||
                                      p.Description.Contains(_searchText, StringComparison.OrdinalIgnoreCase)

            );
        }

        // Category Filter
        if (_selectedCategory != "All")
        {
            filtered = filtered.Where(p => p.Category?.Name == SelectedCategory);
        }

        // 3. Status Tabs Logic (Mapped to existing properties)
        switch (SelectedStatus)
        {
            case 1: // Available
                filtered = filtered.Where(p => p.Qty > 0);
                break;

            case 2: // Offers (Placeholder logic: e.g., Price < 100 or specific ID), Since we don't have 'IsOffer', let's just show items under $500 as an example
                filtered = filtered.Where(p => p.Price < 15);
                break;

            case 3: // New (Placeholder logic: e.g., Last 5 items), Taking the last added items by ID usually implies newness
                filtered = filtered.OrderByDescending(p => p.Id).Take(5);
                break;
        }

        // Convertir a lista para contar
        var allResult = filtered.ToList();

        //Calcular Total de Páginas,Ejemplo: 11 productos / 8 por página = 1.37 -> Ceiling sube a 2 páginas
        totalPages = (int)Math.Ceiling((double)allResult.Count / pageSize);

        // 4. Validar Página Actual (Evita estar en página 5 si solo hay 1 página de resultados)
        if (currentPage > totalPages && totalPages > 0)
        {
            currentPage = totalPages;
        }

        if (totalPages == 0)
        {
            currentPage = 1;
        }

        // Aplicar pagination (Cortar la lista)
        return allResult.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
    }

    // Evento del componente hijo (Pagination)
    private void PageChanged(int newPage)
    {
        currentPage = newPage;
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