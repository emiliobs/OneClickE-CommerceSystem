using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;

namespace OneClick.Frontend.Pages.Categories
{
    public partial class Categories
    {
        //INJECTIONS (Services)
        [Inject]
        public ICategoryService CategoryService { get; set; } = default!;

        [Inject]
        public AlertService SweetAlertService { get; set; } = default!;

        private List<Category> categories = new();

        // Store only the categories currently visible to the user
        private List<Category> filteredCategories = new();

        private Category currentCategory = new();

        // Search text variable.
        private string searchText = "";

        // UI State Flags
        private bool isLoading = true;

        private bool isUploading = false;

        private bool showFormModal = false;

        private bool isEditing = false; // Flags if we  are updating or creating

        private bool isSaving = false; // Controls the spinner

        // --- PAGINATION VARIABLES (NEW) ---
        private int currentPage = 1;

        private int itemsPerPage = 7;

        public int TotalPages => filteredCategories.Count == 0 ? 1 : (int)Math.Ceiling((double)filteredCategories.Count / itemsPerPage);

        // Calculated Property: Gets ONLY the records for the current page
        public IEnumerable<Category> PaginatedCategories
        {
            // Logic: Skip previous pages and Take the next chunk
            get
            {
                return filteredCategories.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage);
            }
        }

        // Initialization
        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
        }

        private async Task LoadCategories()
        {
            isLoading = true;
            StateHasChanged();

            try
            {
                // Fetch data form API
                categories = await CategoryService.GetAllCategoryAsync();

                // Initialize the filtered list with All data (because search is empty at start)
                filteredCategories = new List<Category>(categories);

                isLoading = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SweetAlertService.ShowErrorAlert("Error", $"{ex.Message}");
            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }

        // ----- Search Logic ---
        private void FilterCategories(ChangeEventArgs e)
        {
            // Get the text from the input
            searchText = e.Value?.ToString() ?? "";

            // Filter logic
            if (string.IsNullOrWhiteSpace(searchText))
            {
                // If search is empty, restore the full list
                filteredCategories = new List<Category>(categories);
            }
            else
            {
                // Filter by Name (Case Insentitive), Example: "bo" finds "Books" and "Boots"
                filteredCategories = categories.Where(c => c.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                                               .ToList();
            }

            // IMPORTANT: Always reset to page 1 when searching to avoid empty pages
            currentPage = 1;
        }

        // This method is called by the Component when a button is clicked
        private void ChangePage(int newPage)
        {
            currentPage = newPage;
        }

        private void ClearSearch()
        {
            searchText = string.Empty;
            filteredCategories = new List<Category>();
            currentPage = 1;
        }

        // ---- Form Logic ----
        private void ShowAddForm()
        {
            // Reset the object to be empty fo anew category
            currentCategory = new Category();
            // FIX: Explicitly set isEditing to false.
            // Otherwise, if you clicked "Edit" before, this stays true!
            isEditing = false;
            showFormModal = true;
        }

        private void EditCategory(Category category)
        {
            currentCategory = new Category
            {
                Id = category.Id,
                Name = category.Name,
            };

            // Set the flag to true
            isEditing = true;

            showFormModal = true;
        }

        private void CloseFormModel()
        {
            showFormModal = false;
        }

        private async Task SaveCategory(EditContext context)
        {
            // Manual Validation: Check for errors first
            if (!context.Validate())
            {
                var error = context.GetValidationMessages().ToList();
                var errorHTML = string.Join("</br>", error);

                //showFormModal errors in a nice alert instead of red text
                SweetAlertService.ShowErrorAlert("Error", errorHTML);

                return;
            }

            //  If valid, proceed to Server
            isSaving = true;
            try
            {
                bool success;
                StateHasChanged();

                if (isEditing)
                {
                    success = await CategoryService.UpdateCategoryAsync(currentCategory);
                }
                else
                {
                    var created = await CategoryService.AddCategoryAsync(currentCategory);
                    success = created != null;
                }

                if (success)
                {
                    showFormModal = false;

                    await LoadCategories(); // Refrest list

                    await SweetAlertService.ShowSuccessToast(isEditing ? "Category Update!" : "Category Created!");
                }
                else
                {
                    await SweetAlertService.ShowErrorAlert("Error", "Operation failed check your data.");
                }
            }
            catch (Exception ex)
            {
                await SweetAlertService.ShowErrorAlert("System Error", $"{ex.Message}");
            }
            finally
            {
                isSaving = false;
            }
        }

        private async Task DeleteCategory(Category category)
        {
            // Confirm with the user var
            var confirmed = await SweetAlertService.ConfirmAsync(
                  "Are you sure",
                  $"You won't be able to revert this! Deleting" +
                  $": {category.Name}!"
                );

            // If confirm, proceed to delete
            if (confirmed)
            {
                var success = await CategoryService.DeleteCategoryAsync(category.Id);

                if (success)
                {
                    await LoadCategories();
                    await SweetAlertService.ShowSuccessToast("Category deleted successfully!");
                }
                else
                {
                    await SweetAlertService.ShowErrorAlert("Cannot Delete",
                         "This category contains products. Please delete the products first.");
                }
            }
        }
    }
}