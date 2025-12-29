using Microsoft.JSInterop;

namespace OneClick.Frontend.Services;

public class SweetAlertService
{
    private readonly IJSRuntime _jsRuntime;

    public SweetAlertService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    // Method to show a success toast notification
    public async Task ShowSuccessToast(string message)
    {
        await _jsRuntime.InvokeVoidAsync("eval", $@"
                Swal.fire({{
                    toast: true,
                    position: 'top-end',
                    icon: 'success',
                    title: '{message}',
                    showConfirmButton: false,
                    timer: 3000,
                    timerProgressBar: true
                }})");
    }

    // Method to show an error alert (useful for deletion errors)
    public async Task ShowErrorAlert(string title, string message)
    {
        await _jsRuntime.InvokeVoidAsync("Swal.fire", title, message, "error");
    }
}