using Microsoft.AspNetCore.Components;
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

    protected override async Task OnInitializedAsync()
    {
        try
        {
            isLoading = true;

            // The interceptor attaches the token automatically
            userProfile = await Http.GetFromJsonAsync<UserProfileDTO>("api/user/my-profile");
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
}