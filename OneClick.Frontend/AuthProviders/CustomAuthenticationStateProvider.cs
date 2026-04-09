using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace OneClick.Frontend.AuthProviders;

// This class inherits from Blazor's built-in AuthenticationStateProvider
public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;
    private readonly HttpClient _httpClient;

    // Inject JS to access LocalStorage, and HttpClient to attach the token to future requests
    public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, HttpClient httpClient)
    {
        _jsRuntime = jsRuntime;
        _httpClient = httpClient;
    }

    // 1. This method runs automatically to check if the user is currently logged in
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            // Try to read the token from the browser's memory
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

            // If there is no token, the user is an anonymous guest
            if (string.IsNullOrWhiteSpace(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // If there is a token, attach it to the HttpClient so the API trusts us
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Decode the token to get the user's details and return them
            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[AuthProvider] Error loading authentication state: {ex.Message}");
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    // 2. We call this method exactly when the user types the correct password
    public async Task MarkUserAsAuthenticated(string token)
    {
        try
        {
            // Save the token securely in the browser
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);

            // Tell Blazor to update the UI immediately
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[AuthProvider] Error marking user as authenticated: {ex.Message}");
        }
    }

    // 3. We call this method when the user clicks "Logout"
    public async Task MarkUserAsLoggedOut()
    {
        try
        {
            // Delete the token from the browser
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");

            // Remove the token from our HTTP requests
            _httpClient.DefaultRequestHeaders.Authorization = null;

            // Tell Blazor to update the UI to show the "Login" button again
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[AuthProvider] Error logging out: {ex.Message}");
        }
    }

    // --- HELPER METHODS TO DECODE THE JWT (Standard Boilerplate) ---
    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs != null)
        {
            // 1. Check for the Role claim (Support both long schema and short "role" name)
            keyValuePairs.TryGetValue(ClaimTypes.Role, out object? roles);
            if (roles == null) keyValuePairs.TryGetValue("role", out roles);

            if (roles != null)
            {
                if (roles.ToString()!.Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString()!);
                    foreach (var parsedRole in parsedRoles!)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()!));
                }
                // Clean up to avoid duplicates in the next AddRange
                keyValuePairs.Remove(ClaimTypes.Role);
                keyValuePairs.Remove("role");
            }

            // 2. Check for the NameIdentifier (UserId) - Support short "sub" or long nameid
            keyValuePairs.TryGetValue(ClaimTypes.NameIdentifier, out object? nameid);
            if (nameid == null) keyValuePairs.TryGetValue("sub", out nameid);
            if (nameid != null)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, nameid.ToString()!));
                keyValuePairs.Remove(ClaimTypes.NameIdentifier);
                keyValuePairs.Remove("sub");
            }

            // 3. Add the rest of the claims automatically
            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)));
        }
        return claims;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}