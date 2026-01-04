using Microsoft.JSInterop;
using CurrieTechnologies.Razor.SweetAlert2;

namespace OneClick.Frontend.Services;

public class SweetAlertService
{
    private readonly CurrieTechnologies.Razor.SweetAlert2.SweetAlertService _sweetAlertService;

    public SweetAlertService(CurrieTechnologies.Razor.SweetAlert2.SweetAlertService sweetAlertService)
    {
        this._sweetAlertService = sweetAlertService;
    }

    // Method to show a success toast notification
    public async Task ShowSuccessToast(string message)
    {
        await _sweetAlertService.FireAsync(new SweetAlertOptions
        {
            Toast = true,
            Position = SweetAlertPosition.TopEnd,
            ShowConfirmButton = false,
            Timer = 3000,
            Icon = SweetAlertIcon.Success,
            Title = message,
            Color = "#fff",
            Background = "#28a745", // Green background
            IconColor = "#fff"
        });
    }

    public async Task<bool> ConfirmAsync(string title, string message)
    {
        var result = await _sweetAlertService.FireAsync(new SweetAlertOptions
        {
            Title = title,
            Text = message,
            Icon = SweetAlertIcon.Warning,
            ShowCancelButton = true,
            Background = "#1a1a2e",
            Color = "#ffffff",

            // --- CAMBIO AQUÍ: Solo acepta true o false ---
            Backdrop = true,

            ConfirmButtonText = "YES, DELETE",
            CancelButtonText = "CANCEL",
            ConfirmButtonColor = "#dc3545",
            CancelButtonColor = "#6c757d"
        });
        return result.IsConfirmed;
    }

    // Method to show an error alert (useful for deletion errors)
    public async Task ShowErrorAlert(string title, string message)
    {
        await _sweetAlertService.FireAsync(new SweetAlertOptions
        {
            Title = title,
            Text = message,
            Icon = SweetAlertIcon.Error,
            ConfirmButtonText = "OK",
            IconColor = "#fff",
            Background = "#dc3545", // Red background
            Color = "#fff"
        });
    }
}