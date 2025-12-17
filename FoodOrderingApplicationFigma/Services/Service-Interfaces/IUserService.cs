using FoodOrderingApplicationFigma.DTOs;
using FoodOrderingApplicationFigma.Models;

namespace FoodOrderingApplicationFigma.Services
{
    public interface IUserService
    {
        Task<IEnumerable<GetAllUserDTO>> GetAllUsers();
        Task<GetAllUserDTO?> GetUserById(int id);
        Task<GetAllUserDTO> InsertUser(CreateUserDTO dto);
        Task<GetAllUserDTO> UpdateUser(UpdateUserDTO dto);
        Task<GetAllUserDTO> DeleteUser(int id);
        Task<GetAllUserDTO?> GetUserByEmailOrPhone(string emailOrPhone);
    }
}
