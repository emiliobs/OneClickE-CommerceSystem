using OneClick.Shared.DTOs;

namespace OneClick.Frontend.Services;

// The interface defines the contract for our authetication service
public interface IAuthService
{
    Task<bool> RegisterAsync(UserRegisterDTO registerDTO);

    Task<AuthResponseDTO> LoginAsync(UserLoginDTO loginDTO);
}