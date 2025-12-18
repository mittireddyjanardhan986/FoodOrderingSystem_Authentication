using FoodOrderingApplicationFigma.DTOs.AuthDTOs;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;
using FoodOrderingApplicationFigma.Interfaces;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Services.JwtService;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

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
        private readonly IJwtService _jwtService;
        private readonly IAddressService _addressService;

        public AuthController(IAuthService authService, IUsers<User> userRepository, 
            ICustomerService customerService, IRestaurantService restaurantService, IAdminService adminService,
            IJwtService jwtService, IAddressService addressService)
        {
            _authService = authService;
            _userRepository = userRepository;
            _customerService = customerService;
            _restaurantService = restaurantService;
            _adminService = adminService;
            _jwtService = jwtService;
            _addressService = addressService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                var allUsers = await _userRepository.GetAllUsers();
                var user = allUsers.FirstOrDefault(u => u.Email == dto.Identifier || u.Phone == dto.Identifier);

                if (user == null)
                    return Unauthorized(new { message = "User not found" });

                using var hmac = new HMACSHA512(user.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

                if (!computedHash.SequenceEqual(user.PasswordHash))
                    return Unauthorized(new { message = "Invalid password" });

                var token = _jwtService.GenerateToken(user);
                
                object response = new
                {
                    token = token,
                    userId = user.UserId,
                    email = user.Email,
                    fullName = user.FullName,
                    phone = user.Phone,
                    role = user.Role
                };

                if (user.Role == 1)
                {
                    response = new
                    {
                        token = token,
                        userId = user.UserId,
                        email = user.Email,
                        fullName = user.FullName,
                        phone = user.Phone,
                        role = user.Role,
                        adminId = user.UserId
                    };
                }
                else if (user.Role == 3)
                {
                    var restaurant = await _restaurantService.GetRestaurantByUserId(user.UserId);
                    response = new
                    {
                        token = token,
                        userId = user.UserId,
                        email = user.Email,
                        fullName = user.FullName,
                        phone = user.Phone,
                        role = user.Role,
                        restaurantId = restaurant?.RestaurantId
                    };
                }
                else if (user.Role == 2)
                {
                    response = new
                    {
                        token = token,
                        userId = user.UserId,
                        email = user.Email,
                        fullName = user.FullName,
                        phone = user.Phone,
                        role = user.Role,
                        customerId = user.UserId
                    };
                }

                return Ok(response);
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
                var allUsers = await _userRepository.GetAllUsers();
                if (allUsers.Any(u => u.Email == registerDto.Email))
                    return BadRequest(new { message = "Email already exists" });

                using var hmac = new HMACSHA512();
                var user = new User
                {
                    FullName = registerDto.FullName,
                    Email = registerDto.Email,
                    Phone = registerDto.Phone,
                    Role = 2,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                    PasswordSalt = hmac.Key,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };
                var createdUser = await _userRepository.InsertUser(user);
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
                var allUsers = await _userRepository.GetAllUsers();
                if (allUsers.Any(u => u.Email == dto.Email))
                    return BadRequest(new { message = "Email already exists" });

                using var hmac = new HMACSHA512();
                var user = new User
                {
                    FullName = dto.RestaurantName,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Role = 3,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                    PasswordSalt = hmac.Key,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };
                var createdUser = await _userRepository.InsertUser(user);

                var createdRestaurant = await _restaurantService.CreateRestaurant(new DTOs.RestaurantDTOs.CreateRestaurantDTO
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

                return Ok(new { message = "Restaurant registered successfully. Pending admin approval.", userId = createdUser.UserId, restaurantId = createdRestaurant?.RestaurantId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("register-admin")]
        public async Task<ActionResult> RegisterAdmin([FromBody] RegisterDTO registerDto)
        {
            try
            {
                var allUsers = await _userRepository.GetAllUsers();
                if (allUsers.Any(u => u.Email == registerDto.Email))
                    return BadRequest(new { message = "Email already exists" });

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

        [HttpPost("register-delivery-boy")]
        public async Task<ActionResult> RegisterDeliveryBoy([FromBody] RegisterDTO registerDto)
        {
            try
            {
                var allUsers = await _userRepository.GetAllUsers();
                if (allUsers.Any(u => u.Email == registerDto.Email))
                    return BadRequest(new { message = "Email already exists" });

                using var hmac = new HMACSHA512();
                var user = new User
                {
                    FullName = registerDto.FullName,
                    Email = registerDto.Email,
                    Phone = registerDto.Phone,
                    Role = 4,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                    PasswordSalt = hmac.Key,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };
                var createdUser = await _userRepository.InsertUser(user);

                return Ok(new { message = "Delivery boy registered successfully", userId = createdUser.UserId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
