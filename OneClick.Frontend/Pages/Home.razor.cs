namespace OneClick.Frontend.Pages;

public partial class Home
{
    // Variable to store the name of the last product clicked
    private string LastAddedProduct = "";

    // This method receives the product title from the ProductCard component
    private void HandleAddToCart(string productTitle)
    {
        LastAddedProduct = productTitle;
        // In the future, this will add the item to a real shopping cart service
    }
}