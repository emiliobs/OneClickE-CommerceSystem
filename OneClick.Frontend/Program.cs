using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OneClick.Frontend;
using OneClick.Frontend.Services;
using System.Runtime.InteropServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// --- API CONNECTION LOGIC ---

// FORCE CLOUD URL FOR TESTING (Use HTTPS)
/*string apiUrl = "https://emiliobarrera-001-site1.mtempurl.com"; */// <--- Note the 's' in https

string apiUrl;

if (builder.HostEnvironment.IsDevelopment())
{
    // If running locally in Visual Studio
    apiUrl = "https://localhost:7009";
}
else
{
    // If running on the Server (SmarterASP / Azure)
    apiUrl = "https://emiliobarrera-001-site1.mtempurl.com";
}

// Configure HttpClient for backend API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiUrl)
});

// Register services with interface
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();

// Register the notification service
builder.Services.AddScoped<OneClick.Frontend.Services.SweetAlertService>();

builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();