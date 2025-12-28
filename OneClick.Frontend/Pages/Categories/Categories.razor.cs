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

            categories = await _categoryService.GetAllCategoryAsync();

            isLoading = false;
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
                        CloseFormModel();
                        await LoadCategories();
                        ShowAlert("Category update successfully!", "success");
                    }
                    else
                    {
                        ShowAlert("Failed to update category", "error");
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
                        CloseFormModel();
                        await LoadCategories();
                        ShowAlert("CAtegory created successfully!", "success");
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                showError = true;
            }
            finally
            {
                isSaving = false;
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

            isDeleting = true;

            var success = await _categoryService.DeleteCategoryAsync(categoryToDelete.Id);
            if (success)
            {
                CloseDeleteModal();
                await LoadCategories();
                ShowAlert("Category deleted successfully!", "success");
            }
            else
            {
                ShowAlert("FAiled to delete category", "error");
            }

            isDeleting = false;
        }

        private void CloseFormModel()
        {
            if (!isSaving)
            {
                showFormModal = false;
                categoryToDelete = null;
            }
        }

        private void CloseDeleteModal()
        {
            if (!isDeleting)
            {
                showDeleteModal = false;
                categoryToDelete = null;
            }
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