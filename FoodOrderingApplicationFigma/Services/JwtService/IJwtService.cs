using FoodOrderingApplicationFigma.Models;

namespace FoodOrderingApplicationFigma.Services.JwtService
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}