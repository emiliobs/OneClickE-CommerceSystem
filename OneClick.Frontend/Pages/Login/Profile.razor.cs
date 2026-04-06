using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OneClick.Frontend.Services;
using OneClick.Shared.DTOs;
using System.Net.Http.Json;

namespace OneClick.Frontend.Pages.Login;

public partial class Profile
{
    [Inject] private HttpClient Http { get; set; } = default!;
    [Inject] private AlertService SweetAlertService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    // Using the official DTO now!
    private UserProfileDTO? userProfile;

    private bool isLoading = true;
    private bool isEditing = false;
    private bool isSaving = false;

    protected string? _imagePreviewUrl;

    protected override async Task OnInitializedAsync()
    {
        await LoadProfileAsync();
    }

    // This method loads the user's profile data from the API and handles any errors that may occur during the process.
    private async Task LoadProfileAsync()
    {
        try
        {
            isLoading = true;

            // The interceptor attaches the token automatically
            var result = await Http.GetFromJsonAsync<UserProfileDTO>("api/user/my-profile");
            if (result != null)
            {
                userProfile = result;
                _imagePreviewUrl = userProfile.ImageUrl; // Assuming the API returns a URL for the profile image
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
            isLoading = false;
        }
    }

    // // This method toggles the edit mode for the user's profile. When exiting edit mode,
    // it resets the image preview and clears any unsaved image data to ensure that the user does not accidentally
    // save unwanted changes.

    protected void ToogleEditMode()
    {
        isEditing = !isEditing;

        if (!isEditing)
        {
            // If canceling edit mode, reset the image preview and clear any unsaved image data
            _imagePreviewUrl = userProfile!.ImageUrl; // Reset preview to original if canceling edit
            userProfile.ImageBase64 = null; // Clear any unsaved image data
        }
    }

    protected async Task OnFileSelected(InputFileChangeEventArgs e)
    {
        // Get the selected file from the event arguments
        var file = e.File;

        if (file == null)
        {
            return; // No file selected, exit the method
        }

        if (file.Size > 2097152) // 2MB in bytes
        {
            await SweetAlertService.ShowErrorAlert("File Too Large", "Please select an image smaller than 2MB.");
            return;
        }

        if (!file.ContentType.StartsWith("image/"))
        {
            await SweetAlertService.ShowErrorAlert("Invalid File Type", "Please select a valid image file.");
            return;
        }

        try
        {
            // Read the file into a buffer
            var buffer = new byte[file.Size];
            await file.OpenReadStream().ReadAsync(buffer);
            var base64String = Convert.ToBase64String(buffer);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}