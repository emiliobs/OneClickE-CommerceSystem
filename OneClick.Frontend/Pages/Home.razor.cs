using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;
using System.Security.Claims;

namespace OneClick.Frontend.Pages;

public partial class Home : ComponentBase, IDisposable
{
    // Inject required services for UI alerts and data fetching
    [Inject] public AlertService SweetAlertService { get; set; } = default!;

    [Inject] public IProductService ProductService { get; set; } = default!;
    [Inject] public ICartService CartService { get; set; } = default!;
    [Inject] public ICategoryService CategoryService { get; set; } = default!;

    // Inject services required for authentication and routing
    [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    // UI State variables to handle loading spinners and pagination
    private bool isLoading = true;

    private int currentPage = 1;
    private int pageSize = 12; // Shows 12 products per page
    private int totalPages = 0;

    // Filter variables mapped to the component UI
    private string _searchText = "";

    private string _selectedCategory = "All";
    private int _selectedStatus = 0; // 0: All, 1: Available, 2: Offers, 3: New

    // Shopping cart state variables
    private int cartCount = 0;

    private bool showCart = false;
    private Product? _selectedProduct = null;

    // Dynamic User ID variable to handle the authenticated session
    private int currentUserId = 0;

    // Public properties that reset the pagination to page 1 when filter values change
    public string SearchText
    {
        get => _searchText;
        set { _searchText = value; currentPage = 1; }
    }

    public string SelectedCategory
    {
        get => _selectedCategory;
        set { _selectedCategory = value; currentPage = 1; }
    }

    public int SelectedStatus
    {
        get => _selectedStatus;
        set { _selectedStatus = value; currentPage = 1; }
    }

    // Lists to store data fetched from the API
    private List<Product> products = new List<Product>();

    private List<Category> categories = new List<Category>();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            isLoading = true;

            //  Authenticate the session and extract the User ID dynamically
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                // Safely extract the ID checking common claim types
                var idClaim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst("id") ?? user.FindFirst("UserId");
                // Try parsing the ID claim to an integer, defaulting to 0 if parsing fails
                if (idClaim != null && int.TryParse(idClaim.Value, out int parsedId))
                {
                    // Successfully extracted the User ID from the token, assign it to the dynamic variable
                    currentUserId = parsedId;
                }
            }

            // Subscribe to the cart service update event
            CartService.OnChange += UpdateCartCount;

            // Fetch the initial cart count ONLY if a user is successfully logged in
            if (currentUserId > 0)
            {
                cartCount = await CartService.GetCartCountAsync(currentUserId);
            }

            // Fetch the global product and category lists from the database
            products = await ProductService.GetProductsAsync();
            // Add a default "All" category to the list for the dropdown filter
            categories = await CategoryService.GetAllCategoryAsync();
        }
        catch (Exception ex)
        {
            // Catch any potential API errors and display them safely to the user
            await SweetAlertService.ShowErrorAlert("Error loading products:", ex.Message);
            products = new List<Product>(); // Initialize as empty to prevent UI crashes
        }
        finally
        {
            // Always disable the loading spinner regardless of success or failure
            isLoading = false;
        }
    }

    private async void UpdateCartCount()
    {
        // This function triggers automatically when an item is added to the cart.
        // It updates the count only for authenticated users.
        if (currentUserId > 0)
        {
            // Fetch the updated cart count from the backend API to ensure accuracy
            cartCount = await CartService.GetCartCountAsync(currentUserId);
            await InvokeAsync(StateHasChanged); // Force the UI to re-render the bubble
        }
    }

    private async Task ShowCart()
    {
        // Opens the sliding cart component
        showCart = true;
    }

    public void Dispose()
    {
        // Best practice: Unsubscribe from the event to prevent memory leaks
        CartService.OnChange -= UpdateCartCount;
    }

    private List<Product> GetFilteredAndPAginateProducts()
    {
        // Start with the full list of products
        var filtered = products.AsEnumerable();

        // Apply text search filter by product name or description
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            //  Use case-insensitive search to match the user's input against product names and descriptions
            filtered = filtered.Where(p => p.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase) ||
                                           p.Description.Contains(_searchText, StringComparison.OrdinalIgnoreCase));
        }

        // Apply category dropdown filter
        if (_selectedCategory != "All")
        {
            filtered = filtered.Where(p => p.Category?.Name == SelectedCategory);
        }

        // Apply the status tabs logic
        switch (SelectedStatus)
        {
            case 1: // Available in stock
                filtered = filtered.Where(p => p.Qty > 0);
                break;

            case 2: // Offers (Items priced under a certain threshold)
                filtered = filtered.Where(p => p.Price < 15);
                break;

            case 3: // New additions (Taking the last 5 added items)
                filtered = filtered.OrderByDescending(p => p.Id).Take(5);
                break;
        }

        // Convert the filtered results to a list to calculate total pages
        var allResult = filtered.ToList();
        totalPages = (int)Math.Ceiling((double)allResult.Count / pageSize);

        // Validate the current page to prevent empty views
        if (currentPage > totalPages && totalPages > 0) currentPage = totalPages;
        //  If there are no results, reset to page 1 to avoid invalid page numbers
        if (totalPages == 0) currentPage = 1;

        // Apply pagination constraints and return the final subset
        return allResult.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
    }

    private void PageChanged(int newPage)
    {
        // Update the current page index when the user clicks a pagination button
        currentPage = newPage;
    }

    private async Task HandleAddToCart(Product product)
    {
        // SECURITY CHECK: Prevent anonymous (guest) users from adding items to the cart
        if (currentUserId == 0)
        {
            await SweetAlertService.ShowErrorAlert("Login Required", "Please log in or register to start shopping.");
            NavigationManager.NavigateTo("/login");
            return;
        }

        // Validation: Ensure the product is actually in stock
        if (product.Qty <= 0)
        {
            await SweetAlertService.ShowErrorAlert("Out of Stock", "This item is not available.");
            return;
        }

        try
        {
            // Build the cart item object using the dynamic User ID
            var cartItem = new CartItem()
            {
                ProductId = product.Id,
                UserId = currentUserId,
                Quantity = 1
            };

            // Send the new item to the backend API
            await CartService.AddToCartAsync(cartItem);

            // Visual feedback: Deduct the stock locally to reflect changes instantly
            if (product.Qty > 0)
            {
                // This is a local UI update to give immediate feedback. The backend will handle the actual stock deduction.
                product.Qty--;
            }

            // Display a success notification to the user
            await SweetAlertService.ShowSuccessToast($"Added: {product.Name}");
        }
        catch (Exception ex)
        {
            // Catch and display any errors during the add-to-cart process
            await SweetAlertService.ShowErrorAlert("Error", $"Could not add item: {ex.Message}");
        }
    }

    private void ShowProductDetails(Product product)
    {
        // Assign the selected product to trigger the details modal
        _selectedProduct = product;
    }
}