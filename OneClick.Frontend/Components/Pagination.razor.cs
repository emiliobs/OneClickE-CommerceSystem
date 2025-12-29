using Microsoft.AspNetCore.Components;

namespace OneClick.Frontend.Components;

public partial class Pagination
{
    // Data coming FROM the parent (Categories)
    [Parameter]
    public int CurrentPage { get; set; } = 1;

    [Parameter]
    public int TotalPages { get; set; } = 1;

    //Event sending data BACK to the parent
    [Parameter]
    public EventCallback<int> SelectedPage { get; set; }

    // Internal method to handle click and notify parent
    private async Task OnSelectedPage(int page)
    {
        // Trigger the event so the Parent knows the page changed
        if (page >= 1 && page <= TotalPages && page != CurrentPage)
        {
            await SelectedPage.InvokeAsync(page);
        }
    }
}