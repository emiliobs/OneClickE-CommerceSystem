using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OneClick.Frontend;
using OneClick.Frontend.AuthProviders;
using OneClick.Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 1. Register the JwtInterceptor so Blazor can create it
builder.Services.AddTransient<JwtInterceptor>();

// 2. Register the HttpClient and wrap it with our Interceptor
builder.Services.AddScoped(sp =>
{
    // Get the interceptor we just registered
    var interceptor = sp.GetRequiredService<JwtInterceptor>();

    // Assign the default message handler to let requests actually go out
    interceptor.InnerHandler = new HttpClientHandler();

    // Return the custom HttpClient pointing to your API
    return new HttpClient(interceptor)
    {
        // IMPORTANT: Make sure this is your correct Backend port!
        //BaseAddress = new Uri("https://oneclickapi.runasp.net")
        BaseAddress = new Uri("https://localhost:7009/")
    };
});

// Register services with interface
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
// Tell Blazor to use AuthService whenever a component asks for IAuthService
builder.Services.AddScoped<IAuthService, AuthService>();

// This tells Blazor: "When a page asks for SweetAlertService, give them this class."
builder.Services.AddScoped<AlertService>();
builder.Services.AddSweetAlert2();

// Enable core authorization features in Blazor
builder.Services.AddAuthorizationCore();

// Tgell Blazor to user our Custom Guardian to manage the authentication state
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

await builder.Build().RunAsync();