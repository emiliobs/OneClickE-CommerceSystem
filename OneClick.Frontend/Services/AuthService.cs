using OneClick.Shared.DTOs;
using System.Net.Http.Json;

namespace OneClick.Frontend.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AuthResponseDTO> LoginAsync(UserLoginDTO loginDTO)
    {
        try
        {
            // Send the POST request to the API endpoint we created earlier
            var response = await _httpClient.PostAsJsonAsync("api/user/login", loginDTO);

            if (response.IsSuccessStatusCode)
            {
                // Extract the token and message from the JSON response
                var result = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();

                return result!;
            }

            // Return null if the credentials were wrong (401 Unauthorized)
            return null!;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"AuthService Registraction network error: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> RegisterAsync(UserRegisterDTO registerDTO)
    {
        try
        {
            // Send the post request to the API endpoint we created early
            var response = await _httpClient.PostAsJsonAsync("api/user", registerDTO);

            // Return true the API responded with a 200 OK.
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            // LOg the error in the browser console if the network fails
            Console.WriteLine($"AuthService. Registraction network error: {ex.Message}");
            return false;
        }
    }
}