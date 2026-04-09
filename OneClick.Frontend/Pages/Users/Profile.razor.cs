using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OneClick.Frontend.Services;
using OneClick.Shared.DTOs;
using OneClick.Shared.Entities;
using System.Net.Http.Json;

namespace OneClick.Frontend.Pages.Users;

public partial class Profile
{
    [Inject] private HttpClient Http { get; set; } = default!;
    [Inject] private AlertService SweetAlertService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    // Injecting the OrderService allows us to fetch the user's orders if we want to display them on the profile page in the future.
    [Inject] public IOrderService OrderService { get; set; } = default!;

    // Using the official DTO now!
    private UserProfileDTO? userProfile;

    // This list is currently not populated, but it can be used in the future to display the user's recent orders or
    // order history on the profile page.
    private List<Order> userOrders = new List<Order>();

    // These boolean flags are used to manage the UI state of the profile page, such as showing loading indicators,
    // toggling edit mode, and disabling the save button while saving.
    private bool isLoading = true;

    private bool isEditing = false;
    private bool isSaving = false;
    private bool isLoadingOrders = true;
    protected string? _imagePreviewUrl;

    // Pagination properties for the orders list. This allows us to implement pagination if we decide to show the user's
    // orders on the profile page.
    private int currentPage = 1;

    private int itemsPerPage = 5; // Show 5 orders per page for better readability, especially if the user has many orders.

    // This property calculates the subset of orders to display based on the current page and items per page. It uses LINQ to skip the appropriate
    // number of orders and take only the number of orders that should be displayed on the current page.
    public IEnumerable<Order> PaginatedOrders => userOrders
        .Skip((currentPage - 1) * itemsPerPage)
        .Take(itemsPerPage);

    // Propiedad calculada para el total de páginas
    public int TotalPages => userOrders.Count == 0 ? 1 : (int)Math.Ceiling((double)userOrders.Count / itemsPerPage);

    protected override async Task OnInitializedAsync()
    {
        await LoadProfileAsync();
    }

    // This method loads the user's profile data from the API and handles any errors that may occur during the process.
    private async Task LoadProfileAsync()
    {
        try
        {
            // Set loading state to true to show a loading indicator while fetching profile data
            isLoading = true;
            isLoadingOrders = true; // Set orders loading state if we decide to load orders here in the future

            // The interceptor attaches the token automatically
            var result = await Http.GetFromJsonAsync<UserProfileDTO>("api/user/my-profile");
            if (result != null)
            {
                userProfile = result;
                _imagePreviewUrl = userProfile.ImageUrl; // Assuming the API returns a URL for the profile image

                // Optionally, we could load the user's orders here if we want to show them on the profile page
                var ordersResult = await OrderService.GetOrdersByUsersIdAsync(userProfile.Id);
                // If the API call is successful, we populate the userOrders list with the retrieved orders
                userOrders = ordersResult.ToList();
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Profile HTTP Request Error: {ex.Message}");
            await SweetAlertService.ShowErrorAlert("Access Denied", "Your session expired. Please log in again.");
            NavigationManager.NavigateTo("/login");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Profile General Error: {ex.Message}");
            await SweetAlertService.ShowErrorAlert("System Error", "An error occurred loading your dashboard.");
        }
        finally
        {
            // Reset loading states regardless of success or failure to ensure the UI updates correctly
            isLoading = false;
            // Set orders loading state to false after attempting to load orders
            isLoadingOrders = false;
        }
    }

    // // This method toggles the edit mode for the user's profile. When exiting edit mode,
    // it resets the image preview and clears any unsaved image data to ensure that the user does not accidentally
    // save unwanted changes.

    protected void ToggleEditMode()
    {
        isEditing = !isEditing;

        if (!isEditing)
        {
            // If canceling edit mode, reset the image preview and clear any unsaved image data
            _imagePreviewUrl = userProfile!.ImageUrl; // Reset preview to original if canceling edit
            userProfile.ImageBase64 = null; // Clear any unsaved image data
        }
    }

    protected async Task OnFilesSelected(InputFileChangeEventArgs e)
    {
        // Validate file selection
        var file = e.File;
        if (file == null)
        {
            return;
        }

        // Validate file size and type
        if (file.Size > 2097152) // 2MB Limit
        {
            await SweetAlertService.ShowErrorAlert("File Too Large", "Maximum image size is 2MB.");
            return;
        }

        // Validate file type (basic check)
        if (!file.ContentType.StartsWith("image/"))
        {
            await SweetAlertService.ShowErrorAlert("Invalid File", "Please select a valid image file.");
            return;
        }

        try
        {
            // Read file into byte array
            var buffer = new byte[file.Size];
            // Read the file stream into the buffer
            await file.OpenReadStream().ReadAsync(buffer);
            // Convert byte array to base64 string
            var base64String = Convert.ToBase64String(buffer);

            // Save base64 to DTO
            userProfile.ImageBase64 = base64String;

            // Set preview URL to show immediately on screen
            _imagePreviewUrl = $"data:{file.ContentType};base64,{base64String}";

            // UI update to show preview immediately
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Profile Error reading file: {ex.Message}");
            await SweetAlertService.ShowErrorAlert("Error", $"Could not process the selected image: {ex.Message}");
        }
    }

    protected async Task SaveProfile()
    {
        try
        {
            // Set saving state to disable the save button and show a loading indicator
            isSaving = true;

            // The interceptor will attach the token automatically, so we can directly send the DTO to the API
            var response = await Http.PutAsJsonAsync("api/user/my-profile", userProfile);

            // Check if the response indicates success
            if (response.IsSuccessStatusCode)
            {
                // Reload the profile to get the updated data from the server, including the new image URL if it was changed
                await SweetAlertService.ShowSuccessToast("Profile Updated. Your profile has been updated successfully.");

                // Exit edit mode after successful save
                isEditing = false;

                // Refresh profile data to reflect changes
                await LoadProfileAsync(); // Refresh profile data to reflect changes
            }
            else
            {
                // If the response is not successful, read the error message from the response content
                var error = await response.Content.ReadAsStringAsync();
                await SweetAlertService.ShowErrorAlert("Save Failed", $"Failed to update profile: {error}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Profile Save Error: {ex.Message}");
            await SweetAlertService.ShowErrorAlert("Save Error", $"An error occurred while saving your profile: {ex.Message}");
        }
        finally
        {
            // Reset saving state regardless of success or failure
            isSaving = false;
        }
    }

    //  This method changes the current page for pagination. It updates the currentPage variable and calls
    //  StateHasChanged to force a UI refresh so that the new page of orders is displayed.
    private void ChangePage(int newPage)
    {
        currentPage = newPage;
        StateHasChanged(); //
    }
}