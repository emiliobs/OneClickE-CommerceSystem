using CurrieTechnologies.Razor.SweetAlert2;

namespace OneClick.Frontend.Services;

public class AlertService
{
    private readonly SweetAlertService _sweetAlert;

    public AlertService(SweetAlertService sweetAlert)
    {
        this._sweetAlert = sweetAlert;
    }

    // Method to show a success toast notification
    public async Task ShowSuccessToast(string message)
    {
        await _sweetAlert.FireAsync(new SweetAlertOptions
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
        var result = await _sweetAlert.FireAsync(new SweetAlertOptions
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
        await _sweetAlert.FireAsync(new SweetAlertOptions
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

    // Method to show an info alert (useful for "Coming soon" or general info)
    public async Task ShowInfoAlert(string title, string message)
    {
        await _sweetAlert.FireAsync(new SweetAlertOptions
        {
            Title = title,
            Text = message,
            Icon = SweetAlertIcon.Info,
            ConfirmButtonText = "OK",
            Background = "#1a1a2e", // Dark aesthetic matching your theme
            Color = "#ffffff",
            ConfirmButtonColor = "#0d6efd"
        });
    }
}