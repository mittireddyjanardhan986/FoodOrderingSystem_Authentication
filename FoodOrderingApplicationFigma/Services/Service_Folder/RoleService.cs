using AutoMapper;
using FoodOrderingApplicationFigma.DTOs.RoleDTOs;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Repository.Repository_Interface;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;

namespace FoodOrderingApplicationFigma.Services.Service_Folder
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetRoleDTO>> GetAllRoles()
        {
            var roles = await _roleRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<GetRoleDTO>>(roles);
        }

        public async Task<GetRoleDTO?> GetRoleById(int id)
        {
            var role = await _roleRepository.GetUserById(id);
            if (role == null) throw new Exception("Role Not Found");
            return _mapper.Map<GetRoleDTO>(role);
        }

        public async Task<GetRoleDTO> CreateRole(CreateRoleDTO createRoleDTO)
        {
            var role = new Role
            {
                RoleName = createRoleDTO.RoleName,
                Description = createRoleDTO.Description
            };
            var createdRole = await _roleRepository.InsertUser(role);
            return _mapper.Map<GetRoleDTO>(createdRole);
        }

        public async Task<GetRoleDTO?> UpdateRole(UpdateRoleDTO updateRoleDTO)
        {
            var existing = await _roleRepository.GetUserById(updateRoleDTO.RoleId);
            if (existing == null) throw new Exception("Role Not Found");
            
            var role = _mapper.Map<Role>(updateRoleDTO);
            var updatedRole = await _roleRepository.UpdateUser(role);
            return _mapper.Map<GetRoleDTO>(updatedRole);
        }

        public async Task<GetRoleDTO> DeleteRole(int id)
        {
            var role = await _roleRepository.GetUserById(id);
            if (role == null) throw new Exception("Role Not Found");
            
            await _roleRepository.DeleteUser(id);
            return _mapper.Map<GetRoleDTO>(role);
        }
    }
}