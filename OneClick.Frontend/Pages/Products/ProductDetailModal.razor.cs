using Microsoft.AspNetCore.Components;
using OneClick.Frontend.Services;
using OneClick.Shared.Entities;

namespace OneClick.Frontend.Pages.Products;

public partial class ProductDetailModal
{
    [Inject]
    public ICartService CartService { get; set; } = default!;

    [Inject]
    public AlertService SweetAlertService { get; set; } = default!;

    [Parameter]
    public Product? Product { get; set; }

    [Parameter]
    public EventCallback OnClose { get; set; }

    // For simplicity, we hardcode the user ID here. In a real application, you would get this from the authentication context.
    [Parameter]
    public int UserId { get; set; }

    private bool isProcessing = false;

    private async Task HandledAddToCart()
    {
        // Prevent multiple clicks
        if (Product == null)
        {
            return;
        }

        isProcessing = true; // Lock button

        try
        {
            // Create CartItem based on the product and user
            var cartItem = new CartItem
            {
                ProductId = Product.Id,
                UserId = UserId, // In a real app, get this from auth context
                Quantity = 1
            };

            // Call the Bridge
            await CartService.AddToCartAsync(cartItem);

            // Visual Feedback: Decrease stock locally immediately, This matches the logic we added in Home.razor
            if (Product.Qty > 0)
            {
                Product.Qty--;
            }

            // Show Feedback
            await SweetAlertService.ShowSuccessToast("Added to cart!");

            // Close the modal automatically so user can keep browsing
            await OnClose.InvokeAsync();
        }
        catch (Exception ex)
        {
            await SweetAlertService.ShowErrorAlert("Error", ex.Message);
        }
        finally
        {
            isProcessing = false; // Unlock button
        }
    }
}