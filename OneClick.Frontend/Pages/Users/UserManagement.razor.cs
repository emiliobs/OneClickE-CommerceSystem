using Microsoft.AspNetCore.Components;
using OneClick.Frontend.Services;
using OneClick.Shared.DTOs;
using System.Net.Http.Json;

namespace OneClick.Frontend.Pages.Users;

public partial class UserManagement : ComponentBase
{
    // INJECTIONS (Services)
    [Inject]
    public HttpClient Http { get; set; } = default!;

    [Inject]
    public AlertService AlertService { get; set; } = default!;

    // Data lists
    protected List<UserProfileDTO> users = new();

    // Store only the users currently visible to the admin (filtered)
    protected List<UserProfileDTO> filteredUsers = new();

    // UI State Flags
    protected bool isLoading = true;

    protected bool showFormModal = false;
    protected bool isSaving = false;

    // Search text variable
    protected string searchText = string.Empty;

    // Form Model for user creation
    protected UserRegisterDTO currentUser = new();

    // Variables for the Edit Role Modal
    protected bool showRoleModal = false;

    protected UserProfileDTO? selectedUserToEdit = null;
    protected string selectedRole = string.Empty;

    // --- PAGINATION VARIABLES ---
    protected int currentPage = 1;

    protected int pageSize = 5; // Shows 5 users per page

    // Computed property to calculate total pages based on the filtered user list
    protected int TotalPages => filteredUsers.Count == 0 ? 1 : (int)Math.Ceiling((double)filteredUsers.Count / pageSize);

    // Computed property to get only the items for the current page chunk
    protected IEnumerable<UserProfileDTO> PaginatedUsers =>
        filteredUsers.Skip((currentPage - 1) * pageSize).Take(pageSize);

    // Initialization method called when the component loads
    protected override async Task OnInitializedAsync()
    {
        await LoadUsersAsync();
    }

    private async Task LoadUsersAsync()
    {
        try
        {
            isLoading = true;

            // Call the API to get all users using the official DTO
            var result = await Http.GetFromJsonAsync<List<UserProfileDTO>>("api/user/GetAllUsers");

            // Check if the result is not null before assigning it to the lists
            if (result != null)
            {
                users = result;
                // Initially, the filtered list is an exact copy of all users
                filteredUsers = new List<UserProfileDTO>(users);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[UserManagement] Error: {ex.Message}");
            await AlertService.ShowErrorAlert("Error", "Could not load the user list.");
        }
        finally
        {
            // Ensure that the loading state is turned off regardless of success or failure
            isLoading = false;
        }
    }

    // ----- Search Logic (Matches Categories Pattern) -----
    protected void FilterUsers(ChangeEventArgs e)
    {
        // Extract the typed text from the input event
        searchText = e.Value?.ToString() ?? string.Empty;

        // If the search text is empty, restore the full list
        if (string.IsNullOrWhiteSpace(searchText))
        {
            filteredUsers = new List<UserProfileDTO>(users);
        }
        else
        {
            // Filter by first name, last name, or email (Case Insensitive)
            filteredUsers = users.Where(u =>
                (u.FirstName != null && u.FirstName.Contains(searchText, StringComparison.OrdinalIgnoreCase)) ||
                (u.LastName != null && u.LastName.Contains(searchText, StringComparison.OrdinalIgnoreCase)) ||
                (u.Email != null && u.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }

        // IMPORTANT: Always reset to the first page when a new search is performed
        currentPage = 1;
    }

    // Triggered by the Pagination component to navigate pages
    protected void ChangePage(int page)
    {
        currentPage = page;
    }

    // ---- Add User Form Logic ----
    protected void ShowAddForm()
    {
        // Reset the form model to be empty for a new user
        currentUser = new UserRegisterDTO();
        showFormModal = true;
    }

    protected void CloseFormModal()
    {
        // Hide the modal
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

            // Check if the response indicates success
            if (response.IsSuccessStatusCode)
            {
                await AlertService.ShowSuccessToast("User created successfully!");
                CloseFormModal();

                // Reload the table to show the newly created user
                await LoadUsersAsync();
            }
            else
            {
                // Read the specific error message from the response content
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
            // Disable the saving spinner
            isSaving = false;
        }
    }

    // ---- Edit Role Logic ----
    protected void OpenEditRoleModal(UserProfileDTO userProfileDTO)
    {
        // Set the selected user to edit
        selectedUserToEdit = userProfileDTO;

        // Pre-select their current role in the dropdown safely
        selectedRole = userProfileDTO.Role ?? "Customer";

        // Open the role modal
        showRoleModal = true;
    }

    protected void CloseRoleModal()
    {
        // Reset the selected user and close the modal
        showRoleModal = false;
        selectedUserToEdit = null;
    }

    // Submit handler for the Edit Role form
    protected async Task SaveRoleAsync()
    {
        // Validate that a user is actually selected before proceeding
        if (selectedUserToEdit is null)
        {
            return;
        }

        try
        {
            // Activate the saving spinner
            isSaving = true;

            // Create the DTO with the user's email and their new role
            var roleUpdateDTO = new UserRoleDTO
            {
                Email = selectedUserToEdit.Email,
                Role = selectedRole
            };

            // Call the API to update the role in the database
            var response = await Http.PutAsJsonAsync("api/user/change-role", roleUpdateDTO);

            if (response.IsSuccessStatusCode)
            {
                await AlertService.ShowSuccessToast("Role updated successfully!");
                CloseRoleModal();

                // Reload the user list to reflect the updated role on the UI
                await LoadUsersAsync();
            }
            else
            {
                await AlertService.ShowErrorAlert("Action Denied", "You cannot change your own role.");
                CloseRoleModal();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[UserManagement] Role save error: {ex.Message}");
            await AlertService.ShowErrorAlert("Error", $"An unexpected error occurred: {ex.Message}");
        }
        finally
        {
            // Disable the saving spinner
            isSaving = false;
        }
    }

    // ---- Delete User Logic ----
    protected async Task DeleteUserAsync(string email)
    {
        // Show a confirmation dialog to the admin before permanent deletion
        var isConfirmed = await AlertService.ConfirmAsync(
            "Delete User",
            $"Are you sure you want to permanently delete {email}?"
        );

        // If the admin cancels, exit the method
        if (!isConfirmed)
        {
            return;
        }

        try
        {
            // Call the API to delete the user using their email address
            var response = await Http.DeleteAsync($"api/user/delete/{email}");

            if (response.IsSuccessStatusCode)
            {
                await AlertService.ShowSuccessToast("User deleted successfully!");
                // Reload the table so the deleted user disappears
                await LoadUsersAsync();
            }
            else
            {
                await AlertService.ShowErrorAlert("Deletion Failed", "Could not delete the user. Please try again.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[UserManagement] Delete error: {ex.Message}");
            await AlertService.ShowErrorAlert("Error", $"An unexpected error occurred: {ex.Message}");
        }
    }
}