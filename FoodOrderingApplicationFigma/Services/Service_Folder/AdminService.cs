using AutoMapper;
using FoodOrderingApplicationFigma.DTOs.AdminDTOs;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Repository.Repository_Interface;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;

namespace FoodOrderingApplicationFigma.Services.Service_Folder
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public AdminService(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAdminDTO>> GetAllAdmins()
        {
            var admins = await _adminRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<GetAdminDTO>>(admins);
        }

        public async Task<GetAdminDTO?> GetAdminById(int id)
        {
            var admin = await _adminRepository.GetUserById(id);
            if (admin == null) throw new Exception("Admin Not Found");
            return _mapper.Map<GetAdminDTO>(admin);
        }

        public async Task<GetAdminDTO> CreateAdmin(CreateAdminDTO createAdminDTO)
        {
            var admin = _mapper.Map<Admin>(createAdminDTO);
            var createdAdmin = await _adminRepository.InsertUser(admin);
            return _mapper.Map<GetAdminDTO>(createdAdmin);
        }

        public async Task<GetAdminDTO?> UpdateAdmin(UpdateAdminDTO updateAdminDTO)
        {
            var existing = await _adminRepository.GetUserById(updateAdminDTO.AdminId);
            if (existing == null) throw new Exception("Admin Not Found");
            
            var admin = _mapper.Map<Admin>(updateAdminDTO);
            var updatedAdmin = await _adminRepository.UpdateUser(admin);
            return _mapper.Map<GetAdminDTO>(updatedAdmin);
        }

        public async Task<GetAdminDTO> DeleteAdmin(int id)
        {
            var admin = await _adminRepository.GetUserById(id);
            if (admin == null) throw new Exception("Admin Not Found");
            
            await _adminRepository.DeleteUser(id);
            return _mapper.Map<GetAdminDTO>(admin);
        }
    }
}