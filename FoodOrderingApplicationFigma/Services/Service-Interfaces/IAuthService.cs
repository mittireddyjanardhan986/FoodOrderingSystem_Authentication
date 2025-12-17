using FoodOrderingApplicationFigma.DTOs.AuthDTOs;

namespace FoodOrderingApplicationFigma.Services.Service_Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDto);
        Task<AuthResponseDTO?> RegisterAsync(RegisterDTO registerDto);
    }
}