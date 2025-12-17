using FoodOrderingApplicationFigma.DTOs.RoleDTOs;

namespace FoodOrderingApplicationFigma.Services.Service_Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<GetRoleDTO>> GetAllRoles();
        Task<GetRoleDTO?> GetRoleById(int id);
        Task<GetRoleDTO> CreateRole(CreateRoleDTO createRoleDTO);
        Task<GetRoleDTO?> UpdateRole(UpdateRoleDTO updateRoleDTO);
        Task<GetRoleDTO> DeleteRole(int id);
    }
}