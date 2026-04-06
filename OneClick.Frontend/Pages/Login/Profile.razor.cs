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
}