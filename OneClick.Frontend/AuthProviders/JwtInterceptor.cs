using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace OneClick.Frontend.AuthProviders;

// We inherit from DelegatingHandler to intercept HTTP requests
public class JwtInterceptor : DelegatingHandler
{
    private readonly IJSRuntime _jsRuntime;

    // We inject JS Runtime to read the browser's memory
    public JwtInterceptor(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    // This method runs automatically right before ANY HTTP request leaves the browser
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Get the token from LocalStorage
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

            // 2. If the token exists, attach it to the request header
            if (!string.IsNullOrWhiteSpace(token))
            {
                // We format it as a "Bearer" token, which is the industry standard
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine("JwtInterceptor Token successfully attached to request.");
            }
        }
        catch (Exception ex)
        {
            // If something fails, log the error but do not crash the app
            Console.WriteLine($"[JwtInterceptor] Error attaching token: {ex.Message}");
        }

        // 3. Let the HTTP request continue to the API
        return await base.SendAsync(request, cancellationToken);
    }
}