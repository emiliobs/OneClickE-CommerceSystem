using Microsoft.AspNetCore.Components.Web;
using OneClick.Shared.Entities;

namespace OneClick.Frontend.Pages.Categories
{
    public partial class Categories
    {
        // State variables
        private List<Category> categories = new();

        private Category? categoryToDelete;

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

            categories = await _categoryService.GetAllCategoryAsync();

            isLoading = false;
            StateHasChanged();
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

                    var success = await _categoryService.UpdateCategoryAsync(categoryToDelete.Id, categoryToUpdate);

                    if (success)
                    {
                        //  // Reset flag before closing
                        isSaving = false;

                        // cerramos el modal(ahora CloseFormModal obedecerá)
                        CloseFormModel();

                        await LoadCategories();
                        await _sweetAlertService.ShowSuccessToast("Category update successfully!");
                    }
                    else
                    {
                        // Show pretty toast notification
                        await _sweetAlertService.ShowErrorAlert("Error", "Failed to update category. Please try again.");
                    }
                }
                else
                {
                    // Create new category
                    var newCategory = new Category
                    {
                        Name = categoryName,
                    };
                    var createdCategory = await _categoryService.AddCategoryAsync(newCategory);

                    if (createdCategory != null)
                    {
                        // Reset flag before closing
                        isSaving = false;

                        // cerramos el modal(ahora CloseFormModal obedecerá)
                        CloseFormModel();

                        await LoadCategories();
                        await _sweetAlertService.ShowSuccessToast("Category created successfully!");
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

        private void DeleteCategory(Category category)
        {
            categoryToDelete = category;
            showDeleteModal = true;
        }

        private async Task ConfirmDelete()
        {
            if (categoryToDelete is null)
            {
                return;
            }

            // Check if category has products locally before sending request (Optimization)
            if (categoryToDelete.Products != null && categoryToDelete.Products.Count > 0)
            {
                // Show error immediately without calling backend
                await _sweetAlertService.ShowErrorAlert("Cannot Delete",
                         "This category contains products. Please delete the products first.");
                CloseDeleteModal();

                return;
            }

            isDeleting = true;
            StateHasChanged();

            var success = await _categoryService.DeleteCategoryAsync(categoryToDelete.Id);
            if (success)
            {
                // cerramos el modal(ahora CloseFormModal obedecerá)
                CloseDeleteModal();

                await LoadCategories();
                // Show pretty toast notification
                await _sweetAlertService.ShowSuccessToast("Category delete successfully!");
            }
            else
            {
                // If backend failed (likely due to database constraints we missed)
                CloseDeleteModal(); //Close modal to show alert clearly.
                await _sweetAlertService.ShowErrorAlert("Error", "Could not delete category, It might be in use.");
            }

            isDeleting = false;
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