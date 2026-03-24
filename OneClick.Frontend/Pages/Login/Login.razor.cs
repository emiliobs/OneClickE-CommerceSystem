using Microsoft.AspNetCore.Components;
using OneClick.Frontend.Services;
using OneClick.Shared.DTOs;

namespace OneClick.Frontend.Pages.Login;

public partial class Login
{
    [Inject]
    public IAuthService AuthService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public AlertService SweetAlertService { get; set; } = default!;

    // Initialize the DTO to hold user input
    private UserLoginDTO loginModel = new UserLoginDTO();

    private bool isLoading = false;

    private async Task HandleLoginAsync()
    {
        try
        {
            isLoading = true;

            // CAll the authentication service
            var result = await AuthService.LoginAsync(loginModel);

            if (result != null && !string.IsNullOrEmpty(result.Token))
            {
                Console.WriteLine($"Login Success token received: {result.Token}");
                await SweetAlertService.ShowSuccessToast("Login Sucessfully. Welcome back to OneClick!");

                // TODO: Save token and redirect
            }
            else
            {
                // Show an error alert using SweetAlerrt
                await SweetAlertService.ShowErrorAlert("Error", "Invalid email or password");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[LoginView] Error during login process: {ex.Message}");

            await SweetAlertService.ShowErrorAlert("LoginView Error during login process:", $"{ex.Message}");
        }
        finally
        {
            // Always turn off the loading spinner
            isLoading = false;
        }
    }
}