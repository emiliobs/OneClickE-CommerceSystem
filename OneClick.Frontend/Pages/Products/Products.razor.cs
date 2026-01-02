using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;
using System.Threading.Tasks;

namespace OneClick.Frontend.Pages.Products;

public partial class Products
{
    // --- Dependency Injection ---
    [Inject] public IProductService ProductService { get; set; } = default!;

    [Inject]
    public ICategoryService CategoryService { get; set; } = default!;

    [Inject] public Services.SweetAlertService SweetAlertService { get; set; } = default!;

    // --- State Variables ---
    private List<Product> products = new();

    private List<Product> filteredProducts = new();

    // List to polutate the <Select> dropdown
    private List<Category> categories = new();

    // The object we are editing/creating
    private Product productModel = new();

    private string searchText = "";

    // --- Pagination Variables ---
    private int currentPage = 1;

    private int itemsPerPage = 7;

    // Calculates total pages based on filtered results
    public int TotalPages => (int)Math.Ceiling((double)filteredProducts.Count / itemsPerPage);

    // Returns only the slice of products for the current page
    public IEnumerable<Product> PaginatedProducts
    {
        get
        {
            return filteredProducts
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage);
        }
    }

    // --- UI State ---
    private bool isLoading = true;

    private bool isUploading = false;
    private bool showFormModal = false; // Controls MOdal visibility
    private bool isEditing = false; // Flags if we  are updating or creating
    private bool isSaving = false; // Disables buttons while saving

    // --- Initialization ---
    protected override async Task OnInitializedAsync()
    {
        await LoadProducts();
    }

    private async Task LoadProducts()
    {
        isLoading = true;
        StateHasChanged();

        // Fetch all products from API
        products = await ProductService.GetProductsAsync();

        // Initialize filtered list with all products
        filteredProducts = new List<Product>(products);

        isLoading = false;
        StateHasChanged();
    }

    // --- Search Logic ---
    private void FilterProducts(ChangeEventArgs e)
    {
        searchText = e.Value?.ToString() ?? "";

        if (string.IsNullOrWhiteSpace(searchText))
        {
            // Reset filter
            filteredProducts = new List<Product>(products);
        }
        else
        {
            // Filter by Name, Description, or Category Name
            filteredProducts = products
                .Where(p => p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                            (p.Description != null && p.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase)) ||
                            (p.Category?.Name != null && p.Category.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        // Always reset to page 1 after filtering
        currentPage = 1;
    }

    // --- Pagination Event Handler ---
    private void ChangePage(int newPage)
    {
        currentPage = newPage;
    }

    // MOdal and Form Logic
    private async Task ShowAddForm()
    {
        // Reset the model for a new product
        productModel = new Product();

        isEditing = false;

        // Load fresh categories for the dropdown
        categories = await CategoryService.GetAllCategoryAsync();

        // Shoe modal
        showFormModal = true;
    }

    private async Task SaveProduct()
    {
        isSaving = true;
        StateHasChanged();

        try
        {
            bool succes;
            if (isEditing)
            {
                succes = await ProductService.UpdateProductAsync(productModel);
            }
            else
            {
                var created = await ProductService.CreateProductAsync(productModel);
                succes = created != null;
            }

            if (succes)
            {
                showFormModal = false;
                await LoadProducts(); // Refresh list
                await SweetAlertService.ShowSuccessToast(isEditing ? "Product Update!" : "Product Created!");
            }
            else
                await SweetAlertService.ShowErrorAlert("Error", "Operation failed check your data.");
            {
            }
        }
        catch (Exception ex)
        {
            await SweetAlertService.ShowErrorAlert("Error", ex.Message);
        }
        finally
        {
            isSaving = false;
        }
    }

    private async Task EditProduct(Product product)
    {
        // Create a NEW instance (copy) to avoid modifying the table row in real-time
        productModel = new Product
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Qty = product.Qty,
            ImageURL = product.ImageURL,// Shows the existing image in the preview
            CategoryId = product.CategoryId,// This selects the correct dropdown item
        };

        // Set the flag to true so the title says "Edit Product"
        isEditing = true;

        // Load categories for the dropdown
        categories = await CategoryService.GetAllCategoryAsync();

        // Open the modal
        showFormModal = true;
    }

    private async Task DeleteProduct(Product product)
    {
        // Confirm with the user var
        var confirmed = await SweetAlertService.ConfirmAsync(
              "Are you sure",
              $"You won't be able to revert this! Deleting" +
              $": {product.Name}"
            );

        // If confirm, proceed to delete
        if (confirmed)
        {
            var success = await ProductService.DeleteProductAsync(product.Id);

            if (success)
            {
                await LoadProducts();
                await SweetAlertService.ShowSuccessToast("Product deleted successfully!");
            }
            else
            {
                await SweetAlertService.ShowErrorAlert("Error", "Could not delete product. It might be in use");
            }
        }
    }

    private void CloseModal()
    {
        showFormModal = false;
    }

    private async Task HandleImageUpload(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file is null)
        {
            return;
        }

        // This must be TRUE to lock the button
        isUploading = true;

        StateHasChanged(); // Show spinner

        try
        {
            var maxFileSize = 1024 * 1024 * 6;

            using var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream(maxFileSize));
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
            content.Add(fileContent, "file", file.Name);

            // Subir y obtener URL
            var uploadUrl = await ProductService.UploadImageAsync(content);

            // Assignar URL al product
            productModel.ImageURL = uploadUrl;
        }
        catch (Exception ex)
        {
            await SweetAlertService.ShowErrorAlert("Upload Error", ex.Message);
        }
        finally
        {
            // Now we turn it off
            isUploading = false;

            StateHasChanged(); // Hide spinner
        }
    }
}