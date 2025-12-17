using FoodOrderingApplicationFigma.DTOs.AuthDTOs;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;
using FoodOrderingApplicationFigma.Interfaces;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Services.JwtService;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingApplicationFigma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUsers<User> _userRepository;
        private readonly ICustomerService _customerService;
        private readonly IRestaurantService _restaurantService;
        private readonly IAdminService _adminService;
        private readonly IOtpService _otpService;
        private readonly IJwtService _jwtService;
        private readonly IAddressService _addressService;

        public AuthController(IAuthService authService, IUsers<User> userRepository, 
            ICustomerService customerService, IRestaurantService restaurantService, IAdminService adminService,
            IOtpService otpService, IJwtService jwtService, IAddressService addressService)
        {
            _authService = authService;
            _userRepository = userRepository;
            _customerService = customerService;
            _restaurantService = restaurantService;
            _adminService = adminService;
            _otpService = otpService;
            _jwtService = jwtService;
            _addressService = addressService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UnifiedLoginDTO dto)
        {
            try
            {
                // Find all users with this email/phone
                var allUsers = await _userRepository.GetAllUsers();
                var matchingUsers = allUsers.Where(u => u.Email == dto.Identifier || u.Phone == dto.Identifier).ToList();

                if (!matchingUsers.Any())
                    return Unauthorized(new { message = "User not found" });

                // If multiple users found, return role selection
                if (matchingUsers.Count > 1)
                {
                    var roleOptions = matchingUsers.Select(u => new RoleSelectionDTO
                    {
                        UserId = u.UserId,
                        FullName = u.FullName,
                        Email = u.Email,
                        Role = u.Role,
                        RoleName = u.RoleNavigation?.RoleName ?? "Unknown"
                    }).ToList();

                    return Ok(new { 
                        requiresRoleSelection = true, 
                        roles = roleOptions,
                        message = "Multiple accounts found. Please select your role." 
                    });
                }

                // Single user found - proceed with normal login
                var user = matchingUsers.First();

                // Login with Password
                if (!string.IsNullOrWhiteSpace(dto.Password))
                {
                    using var hmac = new HMACSHA512(user.PasswordSalt);
                    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

                    if (!computedHash.SequenceEqual(user.PasswordHash))
                        return Unauthorized(new { message = "Invalid password" });
                }
                // Login with OTP
                else if (!string.IsNullOrWhiteSpace(dto.Otp) && !string.IsNullOrWhiteSpace(dto.Token))
                {
                    var isValidOtp = _otpService.ValidateOtpToken(dto.Token, dto.Otp, dto.Identifier!);
                    if (!isValidOtp)
                        return Unauthorized(new { message = "Invalid or expired OTP" });
                }
                else
                {
                    return BadRequest(new { message = "Provide either password or OTP with token" });
                }

                var token = _jwtService.GenerateToken(user);
                
                // Get RestaurantId if user is Restaurant Owner (Role 2)
                int? restaurantId = null;
                if (user.Role == 2)
                {
                    try
                    {
                        var restaurant = await _restaurantService.GetRestaurantByUserId(user.UserId);
                        restaurantId = restaurant?.RestaurantId;
                    }
                    catch { /* Restaurant not found, keep restaurantId as null */ }
                }

                return Ok(new {
                    requiresRoleSelection = false,
                    authResponse = new AuthResponseDTO
                    {
                        Token = token,
                        Email = user.Email,
                        FullName = user.FullName,
                        Role = user.Role
                    },
                    restaurantId = restaurantId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("register-customer")]
        public async Task<ActionResult> RegisterCustomer([FromBody] RegisterDTO registerDto)
        {
            try
            {
                using var hmac = new HMACSHA512();
                var user = new User
                {
                    FullName = registerDto.FullName,
                    Email = registerDto.Email,
                    Phone = registerDto.Phone,
                    Role = 4, // Customer
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                    PasswordSalt = hmac.Key,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };
                var createdUser = await _userRepository.InsertUser(user);

                var customer = new Customer { user_id = createdUser.UserId };
                await _customerService.CreateCustomer(new DTOs.CustomerDTOs.CreateCustomerDTO { UserId = createdUser.UserId });

                return Ok(new { message = "Customer registered successfully", userId = createdUser.UserId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("register-restaurant")]
        public async Task<ActionResult> RegisterRestaurant([FromBody] RegisterRestaurantDTO dto)
        {
            try
            {
                using var hmac = new HMACSHA512();
                var user = new User
                {
                    FullName = dto.RestaurantName,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Role = 2, // Restaurant Owner
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                    PasswordSalt = hmac.Key,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };
                var createdUser = await _userRepository.InsertUser(user);

                await _restaurantService.CreateRestaurant(new DTOs.RestaurantDTOs.CreateRestaurantDTO
                {
                    UserId = createdUser.UserId,
                    Name = dto.RestaurantName,
                    Description = dto.Description,
                    Address = dto.Address,
                    CityId = dto.CityId,
                    StateId = dto.StateId,
                    Status = "Pending"
                });

                await _addressService.CreateAddress(new DTOs.AddressDTOs.CreateAddressDTO
                {
                    UserId = createdUser.UserId,
                    AddressLine = dto.Address,
                    CityId = dto.CityId,
                    StateId = dto.StateId,
                    IsDefault = true
                });

                return Ok(new { message = "Restaurant registered successfully. Pending admin approval.", userId = createdUser.UserId });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message ?? "No inner exception";
                var stackTrace = ex.StackTrace ?? "No stack trace";
                return StatusCode(500, new { 
                    message = ex.Message, 
                    innerException = innerException,
                    stackTrace = stackTrace
                });
            }
        }

        [HttpPost("register-admin")]
        public async Task<ActionResult> RegisterAdmin([FromBody] RegisterDTO registerDto)
        {
            try
            {
                using var hmac = new HMACSHA512();
                var user = new User
                {
                    FullName = registerDto.FullName,
                    Email = registerDto.Email,
                    Phone = registerDto.Phone,
                    Role = 1,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                    PasswordSalt = hmac.Key,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };
                var createdUser = await _userRepository.InsertUser(user);

                await _adminService.CreateAdmin(new DTOs.AdminDTOs.CreateAdminDTO { UserId = createdUser.UserId });

                return Ok(new { message = "Admin registered successfully", userId = createdUser.UserId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("check-user-exists")]
        public async Task<ActionResult<bool>> CheckUserExists([FromBody] CheckUserExistsDTO dto)
        {
            try
            {
                var allUsers = await _userRepository.GetAllUsers();
                var userExists = allUsers.Any(u => u.Email == dto.Email && u.Role == dto.RoleId);
                return Ok(userExists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("get-user-by-contact")]
        public async Task<ActionResult> GetUserByContact([FromBody] GetUserByContactDTO dto)
        {
            try
            {
                var allUsers = await _userRepository.GetAllUsers();
                var user = allUsers.FirstOrDefault(u => (u.Email == dto.EmailOrPhone || u.Phone == dto.EmailOrPhone) && u.Role == dto.RoleId);
                
                if (user == null)
                    return NotFound(new { message = "User not found" });

                return Ok(new {
                    userId = user.UserId,
                    fullName = user.FullName,
                    email = user.Email,
                    phone = user.Phone,
                    role = user.Role,
                    isActive = user.IsActive,
                    createdAt = user.CreatedAt
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("login-with-role")]
        public async Task<ActionResult<AuthResponseDTO>> LoginWithRole([FromBody] LoginWithRoleDTO dto)
        {
            try
            {
                var user = await _userRepository.GetUserById(dto.SelectedUserId);
                if (user == null)
                    return Unauthorized(new { message = "User not found" });

                // Verify credentials
                if (!string.IsNullOrWhiteSpace(dto.Password))
                {
                    using var hmac = new HMACSHA512(user.PasswordSalt);
                    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

                    if (!computedHash.SequenceEqual(user.PasswordHash))
                        return Unauthorized(new { message = "Invalid password" });
                }
                else if (!string.IsNullOrWhiteSpace(dto.Otp) && !string.IsNullOrWhiteSpace(dto.Token))
                {
                    var isValidOtp = _otpService.ValidateOtpToken(dto.Token, dto.Otp, dto.Identifier!);
                    if (!isValidOtp)
                        return Unauthorized(new { message = "Invalid or expired OTP" });
                }
                else
                {
                    return BadRequest(new { message = "Provide either password or OTP with token" });
                }

                var token = _jwtService.GenerateToken(user);
                
                // Get RestaurantId if user is Restaurant Owner (Role 2)
                int? restaurantId = null;
                if (user.Role == 2)
                {
                    try
                    {
                        var restaurant = await _restaurantService.GetRestaurantByUserId(user.UserId);
                        restaurantId = restaurant?.RestaurantId;
                    }
                    catch { /* Restaurant not found, keep restaurantId as null */ }
                }

                return Ok(new {
                    token = token,
                    email = user.Email,
                    fullName = user.FullName,
                    role = user.Role,
                    restaurantId = restaurantId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}