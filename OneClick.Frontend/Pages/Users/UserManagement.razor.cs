using Microsoft.AspNetCore.Components;
using OneClick.Frontend.Services;
using OneClick.Shared.DTOs;
using System.Net.Http.Json;

namespace OneClick.Frontend.Pages.Users;

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

    // Variables for edit Role Modal
    protected bool showRoleModal = false;

    protected UserProfileDTO? selectedUserToEdit = null;
    protected string selectedRole = string.Empty;

    // Pagination variables
    protected int currentPage = 1;

    protected int pageSize = 5; // Shows 5 users per page

    // Computed property to calculate total pages based on the filtered user list
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
            // The interceptor will attach the token automatically
            isLoading = true;

            // Call the API to get all users using the official DTO
            var result = await Http.GetFromJsonAsync<List<UserProfileDTO>>("api/user/GetAllUsers");

            // Check if the result is not null before assigning it to the users list
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
            // Ensure that the loading state is turned off even if an error occurs
            isLoading = false;
        }
    }

    // Triggered when the user types in the search bar
    protected void FilterUsers()
    {
        // If the search text is empty, show all users
        if (string.IsNullOrWhiteSpace(searchText))
        {
            // If the search text is empty, show all users
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
        // Reset the form and close the modal
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
                await LoadUsersAsync(); // Reload the table to show the new user
            }
            else
            {
                // If the response is not successful, read the error message from the response content
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
            // Ensure that the saving state is turned off even if an error occurs
            isSaving = false;
        }
    }

    // EDIT ROLE LOGIC
    // Open the Edit Role modal and set the selected user and role
    protected void OpenEditRoleModal(UserProfileDTO userProfileDTO)
    {
        // Set the selected user and pre-select their current role in the dropdown
        selectedUserToEdit = userProfileDTO;

        // Pre-select the current role
        selectedRole = userProfileDTO.Role;

        // Open the modal
        showRoleModal = true;
    }

    // Close the Edit Role modal and reset the selected user and role
    protected void CloseRoleModal()
    {
        // Reset the selected user and role
        showRoleModal = false;

        // Clear the selected user and role to reset the form state
        selectedUserToEdit = null;
    }

    // Submit handler for the Edit Role form
    protected async Task SaveRoleAsync()
    {
        // Validate that a user is selected before attempting to save
        if (selectedUserToEdit is null)
        {
            return;
        }

        try
        {
            // Set the saving state to true to disable the form and show a loading indicator
            isSaving = true;

            // Create the DTO to send to the API with the user's email and the new role
            var roleUpdateDTO = new UserRoleDTO
            {
                Email = selectedUserToEdit.Email,
                Role = selectedRole
            };

            // Call the API to update the user's role
            var response = await Http.PutAsJsonAsync("api/user/change-role", roleUpdateDTO);

            // Check if the response indicates success
            if (response.IsSuccessStatusCode)
            {
                // Close the modal and reload the user list to reflect the updated role
                await AlertService.ShowSuccessToast($"Role update successfully!");
                CloseRoleModal();
                // Reload the user list to reflect the updated role
                await LoadUsersAsync();
            }
            else
            {
                // If the response is not successful, read the error message from the response content
                await AlertService.ShowErrorAlert("Action Denied", "You cannot change your own role.");
                CloseRoleModal();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"User Mangement Role save error: {ex.Message}");
            await AlertService.ShowErrorAlert("Error", $"An unexpected error occurred while saving: {ex.Message}");
        }
        finally
        {
            // Ensure that the saving state is turned off even if an error occurs
            isSaving = false;
        }
    }

    // Delete user logic
    protected async Task DeleteUserAsync(string email)
    {
        // Show a confirmation dialog before deleting the user
        var isConfirmed = await AlertService.ConfirmAsync("Delete User", $"Are you sure want to permanentrly delete {email}.");

        // If the user cancels the action, exit the method without doing anything
        if (!isConfirmed)
        {
            // User canceled the deletion, so we simply return without making any API calls
            return;
        }

        // If the user confirms the deletion, proceed to call the API to delete the user
        try
        {
            // Call the API to delete the user by email
            var response = await Http.DeleteAsync($"api/user/delete/{email}");

            // Check if the response indicates success
            if (response.IsSuccessStatusCode)
            {
                await AlertService.ShowSuccessToast("User deleted successfully!");
                await LoadUsersAsync(); // Reload the user list to reflect the deletion
            }
            else
            {
                // If the response is not successful, read the error message from the response content
                await AlertService.ShowErrorAlert("Deletion Failed", "Could not delete the user. Please try again.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"User management Delete error: {ex.Message}");
            await AlertService.ShowErrorAlert("Error", $"An unexpected error occurred while deleting: {ex.Message}");
        }
    }
}