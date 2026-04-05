using Microsoft.AspNetCore.Components;
using OneClick.Frontend.Services;
using OneClick.Shared.DTOs;
using System.Net.Http.Json;

namespace OneClick.Frontend.Pages.Admin;

public partial class UserManagement : ComponentBase
{
    [Inject]
    public HttpClient Http { get; set; } = default!;

    [Inject]
    public AlertService AlertService { get; set; } = default!;

    // Data lists
    protected List<UserProfileDTO> users = new();

    protected List<UserProfileDTO> filteredUsers = new();

    // UI States
    protected bool isLoading = true;

    protected bool showFormModal = false;
    protected bool isSaving = false;
    protected string searchText = string.Empty;

    // Form Model for creation
    protected UserRegisterDTO currentUser = new();

    // Pagination variables
    protected int currentPage = 1;

    protected int pageSize = 5; // Shows 5 users per page
    protected int TotalPages => (int)Math.Ceiling((double)filteredUsers.Count / pageSize);

    // Computed property to get only the items for the current page
    protected IEnumerable<UserProfileDTO> PaginatedUsers =>
        filteredUsers.Skip((currentPage - 1) * pageSize).Take(pageSize);

    protected override async Task OnInitializedAsync()
    {
        await LoadUsersAsync();
    }

    private async Task LoadUsersAsync()
    {
        try
        {
            isLoading = true;
            var result = await Http.GetFromJsonAsync<List<UserProfileDTO>>("api/user/GetAllUsers");

            if (result != null)
            {
                users = result;
                filteredUsers = users; // Initially, filtered is the same as all users
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[UserManagement] Error: {ex.Message}");
            await AlertService.ShowErrorAlert("Error", "Could not load the user list.");
        }
        finally
        {
            isLoading = false;
        }
    }

    // Triggered when the user types in the search bar
    protected void FilterUsers()
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            filteredUsers = users;
        }
        else
        {
            // Filter by first name, last name, or email ignoring casing
            filteredUsers = users.Where(u =>
                (u.FirstName != null && u.FirstName.Contains(searchText, StringComparison.OrdinalIgnoreCase)) ||
                (u.LastName != null && u.LastName.Contains(searchText, StringComparison.OrdinalIgnoreCase)) ||
                (u.Email != null && u.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }

        // Reset to first page when a new search is performed
        currentPage = 1;
    }

    // Triggered by the Pagination component
    protected void ChangePage(int page)
    {
        currentPage = page;
    }

    // Modal Controls
    protected void ShowAddForm()
    {
        currentUser = new UserRegisterDTO(); // Reset the form
        showFormModal = true;
    }

    protected void CloseFormModal()
    {
        showFormModal = false;
    }

    // Submit handler for the New User form
    protected async Task SaveUser()
    {
        try
        {
            isSaving = true;

            // Call your existing registration endpoint
            var response = await Http.PostAsJsonAsync("api/user/register", currentUser);

            if (response.IsSuccessStatusCode)
            {
                await AlertService.ShowSuccessToast("User created successfully!");
                CloseFormModal();
                await LoadUsersAsync(); // Reload the table to show the new user
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                await AlertService.ShowErrorAlert("Registration Failed", error);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[UserManagement] Save Error: {ex.Message}");
            await AlertService.ShowErrorAlert("Error", "An unexpected error occurred while saving.");
        }
        finally
        {
            isSaving = false;
        }
    }
}