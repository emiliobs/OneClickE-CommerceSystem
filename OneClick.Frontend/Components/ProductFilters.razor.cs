using Microsoft.AspNetCore.Components;
using OneClick.Shared.Entities;

namespace OneClick.Frontend.Components;

public partial class ProductFilters
{
    // Parameters received from Parent
    [Parameter] public List<Category> Categories { get; set; } = new();

    [Parameter] public int ResultCount { get; set; }

    // Two-way binding parameters (or simple event callbacks)
    [Parameter] public string SearchText { get; set; } = "";

    [Parameter] public EventCallback<string> SearchTextChanged { get; set; }

    [Parameter] public string SelectedCategory { get; set; } = "All";
    [Parameter] public EventCallback<string> SelectedCategoryChanged { get; set; }

    [Parameter] public int SelectedStatus { get; set; } = 0; // 0:All, 1:Available, 2:Offers, 3:New
    [Parameter] public EventCallback<int> SelectedStatusChanged { get; set; }

    // Handlers to notify parent immediately
    private async Task OnSearchInput(ChangeEventArgs e)
    {
        SearchText = e.Value?.ToString() ?? "";
        await SearchTextChanged.InvokeAsync(SearchText);
    }

    private async Task OnCategoryInput(ChangeEventArgs e)
    {
        SelectedCategory = e.Value?.ToString() ?? "All";
        await SelectedCategoryChanged.InvokeAsync(SelectedCategory);
    }

    private async Task SetStatus(int status)
    {
        SelectedStatus = status;
        await SelectedStatusChanged.InvokeAsync(SelectedStatus);
    }
}