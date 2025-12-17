using FoodOrderingApplicationFigma.DTOs.AdminDTOs;

namespace FoodOrderingApplicationFigma.Services.Service_Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<GetAdminDTO>> GetAllAdmins();
        Task<GetAdminDTO?> GetAdminById(int id);
        Task<GetAdminDTO> CreateAdmin(CreateAdminDTO createAdminDTO);
        Task<GetAdminDTO?> UpdateAdmin(UpdateAdminDTO updateAdminDTO);
        Task<GetAdminDTO> DeleteAdmin(int id);
    }
}