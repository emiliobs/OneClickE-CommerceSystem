using Microsoft.AspNetCore.Components.Web;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace OneClick.Frontend.Pages.Categories
{
    public partial class Categories
    {
        [Inject]
        public ICategoryService CategoryService { get; set; } = default!;

        [Inject]
        public SweetAlertService SweetAlertService { get; set; } = default!;

        private List<Category> categories = new();

        // Store only the categories currently visible to the user
        private List<Category> filteredCategories = new();

        private Category? categoryToDelete;

        // Variable to capture what the user type.
        private string searchText = "";

        // --- PAGINATION VARIABLES (NEW) ---
        private int currentPage = 1;

        private int itemsPerPage = 7; // We can change this to 10

        // Calculated Property: Calculates how many pages we need based on filtered results
        public int TotalPages => (int)Math.Ceiling((double)filteredCategories.Count / itemsPerPage);

        // Calculated Property: Gets ONLY the records for the current page
        public IEnumerable<Category> PaginatedCategories
        {
            // Logic: Skip previous pages and Take the next chunk
            get
            {
                return filteredCategories.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage);
            }
        }

        // UI State flags
        private bool isLoading = true;

        private bool showFormModal = false;
        private bool showDeleteModal = false;
        private bool isEditing = false;
        private bool isSaving = false;
        private bool isDeleting = false;

        private string categoryName = "";
        private string errorMessage = "";
        private bool showError = false;

        // Initialization
        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
        }

        private async Task LoadCategories()
        {
            isLoading = true;
            StateHasChanged();

            // Fetch data form API
            categories = await CategoryService.GetAllCategoryAsync();

            // Initialize the filtered list with All data (because search is empty at start)
            filteredCategories = new List<Category>(categories);

            isLoading = false;
            StateHasChanged();
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
            // No need to validate < 1 here because the component already protects us
            // But keeping validation is good practice.
        }

        // ---- Form Logic ----
        private void ShowAddForm()
        {
            isEditing = false;
            categoryName = "";
            showError = false;
            showFormModal = true;
        }

        private void EditCategory(Category category)
        {
            isEditing = true;
            categoryName = category.Name;
            categoryToDelete = category; // Reuse for editing
            showError = false;
            showFormModal = true;
        }

        private async Task SaveCategory()
        {
            if (!ValidateForm())
            {
                return;
            }

            isSaving = true;
            StateHasChanged();

            try
            {
                if (isEditing && categoryToDelete != null)
                {
                    // Update existing category
                    var categoryToUpdate = new Category
                    {
                        Id = categoryToDelete.Id,
                        Name = categoryName,
                    };

                    var success = await CategoryService.UpdateCategoryAsync(categoryToDelete.Id, categoryToUpdate);

                    if (success)
                    {
                        //  // Reset flag before closing
                        isSaving = false;

                        // cerramos el modal(ahora CloseFormModal obedecerá)
                        CloseFormModel();

                        await LoadCategories();
                        await SweetAlertService.ShowSuccessToast("Category update successfully!");
                    }
                    else
                    {
                        // Show pretty toast notification
                        await SweetAlertService.ShowErrorAlert("Error", "Failed to update category. Please try again.");
                    }
                }
                else
                {
                    // Create new category
                    var newCategory = new Category
                    {
                        Name = categoryName,
                    };
                    var createdCategory = await CategoryService.AddCategoryAsync(newCategory);

                    if (createdCategory != null)
                    {
                        // Reset flag before closing
                        isSaving = false;

                        // cerramos el modal(ahora CloseFormModal obedecerá)
                        CloseFormModel();

                        await LoadCategories();
                        await SweetAlertService.ShowSuccessToast("Category created successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                showError = true;
            }
        }

        private bool ValidateForm()
        {
            showError = false;

            if (string.IsNullOrWhiteSpace(categoryName))
            {
                errorMessage = "Category name is required";
                showError = true;
                return false;
            }

            if (categoryName.Length > 100)
            {
                errorMessage = "Category name cannot exceed 100 chatacters";
                showError = true;
                return false;
            }

            return true;
        }

        private async Task DeleteCategory(Category category)
        {
            // Confirm with the user var
            var confirmed = await SweetAlertService.ConfirmAsync(
                  "Are you sure",
                  $"You won't be able to revert this! Deleting" +
                  $"; {category.Name}!"
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

        private void CloseFormModel()
        {
            showFormModal = false;
            categoryToDelete = null;
        }

        private void CloseDeleteModal()
        {
            showDeleteModal = false;
            categoryToDelete = null;
        }

        private void ShowAlert(string message, string type)
        {
            // Simple JavaScript alert for now
            // We will implement a proper tast notifiaction later
            if (type == "success")
            {
                Console.WriteLine($"Success: {message}");
            }
            else
            {
                Console.WriteLine($"Error: {message}");
            }
        }
    }
}