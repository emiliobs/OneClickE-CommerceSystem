using Microsoft.AspNetCore.Components;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;

namespace OneClick.Frontend.Pages.Products
{
    public partial class Products
    {
        // --- Dependency Injection ---
        [Inject] public IProductService ProductService { get; set; } = default!;

        [Inject] public SweetAlertService SweetAlertService { get; set; } = default!;

        // --- State Variables ---
        private List<Product> products = new();

        private List<Product> filteredProducts = new();
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

        // --- Placeholder Methods for CRUD (To be implemented) ---
        private void ShowAddForm()
        {
            // TODO: Open Modal for creation
        }

        private void EditProduct(Product product)
        {
            // TODO: Open Modal for editing
        }

        private void DeleteProduct(Product product)
        {
            // TODO: Trigger SweetAlert delete confirmation
        }
    }
}