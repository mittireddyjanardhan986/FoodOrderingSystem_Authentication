using FoodOrderingApplicationFigma.DTOs.AuthDTOs;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Repository;
using FoodOrderingApplicationFigma.Services.JwtService;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace FoodOrderingApplicationFigma.Services.Service_Folder
{
    public class AuthService : IAuthService
    {
        private readonly UserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthService(UserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDto)
        {
            var users = await _userRepository.GetAllUsers();
            var user = users.FirstOrDefault(u => u.Email == loginDto.Identifier || u.Phone == loginDto.Identifier);
            
            if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Invalid Credentials");

            var token = _jwtService.GenerateToken(user);
            
            return new AuthResponseDTO
            {
                Token = token,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role,
                Expiration = DateTime.UtcNow.AddHours(24)
            };
        }

        public async Task<AuthResponseDTO?> RegisterAsync(RegisterDTO registerDto)
        {
            var users = await _userRepository.GetAllUsers();
            if (users.Any(u => u.Email == registerDto.Email))
                throw new Exception("Email Already Exists");

            CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                Phone = registerDto.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = registerDto.Role,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var createdUser = await _userRepository.InsertUser(user);
            var token = _jwtService.GenerateToken(createdUser);

            return new AuthResponseDTO
            {
                Token = token,
                Email = createdUser.Email,
                FullName = createdUser.FullName,
                Role = createdUser.Role,
                Expiration = DateTime.UtcNow.AddHours(24)
            };
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
