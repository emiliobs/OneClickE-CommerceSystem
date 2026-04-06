using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using OneClick.Frontend.Services;
using OneClick.Shared.DTOs;
using System.Net.Http.Json;

namespace OneClick.Frontend.Pages.Users;

public partial class Register
{
    [Inject] public HttpClient Http { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    // Injecting SweetAlertService based on your Login code layout
    [Inject] public AlertService SweetAlertService { get; set; } = default!;

    [Inject] public IJSRuntime JSRuntime { get; set; } = default!;

    protected UserRegisterDTO registerModel = new();
    protected bool isRegistering = false;
    protected string? _imagePreviewUrl;

    protected async Task OnFilesSelected(InputFileChangeEventArgs e)
    {
        // Validate file size and type before processing
        var file = e.File;
        if (file == null)
        {
            return;
        }

        // Validate file size (max 2MB) and type (must be an image)
        if (file.Size > 2097152)
        {
            await SweetAlertService.ShowErrorAlert("File Too Large", "Maximum image size is 2MB.");
            return;
        }

        // Validate file type (must be an image)

        if (!file.ContentType.StartsWith("image/"))
        {
            await SweetAlertService.ShowErrorAlert("Invalid File", "Please select a valid image file.");
            return;
        }

        try
        {
            //  Read the file into a byte array and convert to Base64
            var buffer = new byte[file.Size];
            // Using OpenReadStream with a limit to prevent large file issues
            await file.OpenReadStream().ReadAsync(buffer);
            //
            var base64String = Convert.ToBase64String(buffer);

            // Store the Base64 string in the register model and prepare the preview URL
            registerModel.ImageBase64 = base64String;

            // Set the image preview URL to display the selected image immediately
            _imagePreviewUrl = $"data:{file.ContentType};base64,{base64String}";

            //  Trigger UI update to show the preview immediately
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Register] Error reading file: {ex.Message}");
            await SweetAlertService.ShowErrorAlert("Error", "Could not process the selected image.");
        }
    }

    // This method handles the registration process, including auto-login after successful registration
    protected async Task HandleRegistration()
    {
        // Prevent multiple submissions
        isRegistering = true;
        try
        {
            // 1. Call the backend to register the user
            var response = await Http.PostAsJsonAsync("api/user/register", registerModel);

            if (response.IsSuccessStatusCode)
            {
                // Prepare the Login DTO with the exact same credentials
                var loginModel = new UserLoginDTO
                {
                    Email = registerModel.Email,
                    Password = registerModel.Password
                };

                //  Call the login endpoint to get the token
                var loginResponse = await Http.PostAsJsonAsync("api/user/login", loginModel);

                if (loginResponse.IsSuccessStatusCode)
                {
                    // Use the public LoginResponseDTO from your Shared project
                    var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDTO>();

                    if (loginResult != null && !string.IsNullOrEmpty(loginResult.Token))
                    {
                        // Save the token and redirect to Home
                        await JSRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", loginResult.Token);
                        await SweetAlertService.ShowSuccessToast("Welcome to OneClick!");
                        NavigationManager.NavigateTo("/", forceLoad: true);
                    }
                }
                else
                {
                    // Fallback
                    await SweetAlertService.ShowSuccessToast("Registration successful! Please login.");
                    NavigationManager.NavigateTo("/login");
                }
            }
            else
            {
                await SweetAlertService.ShowErrorAlert("Registration Failed", "Please check your information or use a different email.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Register] Error connecting to API: {ex.Message}");
            await SweetAlertService.ShowErrorAlert("Connection Error", "Could not connect to the server.");
        }
        finally
        {
            // Reset the registration state to allow for new attempts
            isRegistering = false;
        }
    }
}