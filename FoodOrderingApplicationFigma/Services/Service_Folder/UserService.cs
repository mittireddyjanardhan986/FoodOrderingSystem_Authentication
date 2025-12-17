using System.Security.Cryptography;
using System.Text;
using FoodOrderingApplicationFigma.DTOs;
using FoodOrderingApplicationFigma.DTOs.AddressDTOs;
using FoodOrderingApplicationFigma.DTOs.SampleDTOs;
using FoodOrderingApplicationFigma.Interfaces;
using FoodOrderingApplicationFigma.Models;

namespace FoodOrderingApplicationFigma.Services
{
    public class UserService : IUserService
    {
        private readonly IUsers<User> repo;

        public UserService(IUsers<User> repos)
        {
            repo = repos;
        }

        public async Task<IEnumerable<GetAllUserDTO>> GetAllUsers()
        {
            var users = await repo.GetAllUsers();
            return users.Select(MapToDTO).ToList();
        }

        public async Task<GetAllUserDTO?> GetUserById(int id)
        {
            var user = await repo.GetUserById(id);
            if (user == null) throw new Exception("User Not Found");
            return MapToDTO(user);
        }

        public async Task<GetAllUserDTO> InsertUser(CreateUserDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Password is required.");

            using var hmac = new HMACSHA512();
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                Role = dto.Role,
                IsActive = true,
                CreatedAt = DateTime.Now,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            var createdUser = await repo.InsertUser(user);
            return MapToDTO(createdUser);
        }

        public async Task<GetAllUserDTO> UpdateUser(UpdateUserDTO dto)
        {
            var existingUser = await repo.GetUserById(dto.UserId);
            if (existingUser == null) throw new Exception("User Not Found");

            if (!string.IsNullOrWhiteSpace(dto.FullName)) existingUser.FullName = dto.FullName;
            if (!string.IsNullOrWhiteSpace(dto.Email)) existingUser.Email = dto.Email;
            if (!string.IsNullOrWhiteSpace(dto.Phone)) existingUser.Phone = dto.Phone;
            if (dto.Role > 0) existingUser.Role = dto.Role;
            if (dto.IsActive.HasValue) existingUser.IsActive = dto.IsActive.Value;

            if (!string.IsNullOrWhiteSpace(dto.password))
            {
                using var hmac = new HMACSHA512();
                existingUser.PasswordSalt = hmac.Key;
                existingUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.password));
            }

            var updatedUser = await repo.UpdateUser(existingUser);
            return MapToDTO(updatedUser);
        }

        public async Task<GetAllUserDTO> DeleteUser(int id)
        {
            var user = await repo.GetUserById(id);
            if (user == null) throw new Exception("User Not Found");
            
            await repo.DeleteUser(id);
            return MapToDTO(user);
        }

        public async Task<GetAllUserDTO?> GetUserByEmailOrPhone(string emailOrPhone)
        {
            var user = await repo.GetUserByEmailOrPhone(emailOrPhone);
            if (user == null) throw new Exception("User Not Found");
            return MapToDTO(user);
        }

        private GetAllUserDTO MapToDTO(User u)
        {
            return new GetAllUserDTO
            {
                UserId = u.UserId,
                FullName = u.FullName,
                Email = u.Email,
                Phone = u.Phone,
                password=u.PasswordHash,
                RoleName = u.RoleNavigation?.RoleName ?? "Unknown",
                CreatedAt = u.CreatedAt,
                IsActive = u.IsActive,

                Addresses = u.Addresses?.Select(a => new GetAddressDTO
                {
                    AddressId = a.AddressId,
                    AddressLine = a.AddressLine ?? "",
                    CityName = a.City?.CityName ?? "",
                    StateName = a.State?.StateName ?? "",
                    IsDefault = a.IsDefault ?? false
                }).ToList(),
            };
        }
    }
}
