using FoodOrderingApplicationFigma.DTOs.ProfileDTOs;
using FoodOrderingApplicationFigma.DTOs.CommonDTOs;
using FoodOrderingApplicationFigma.Interfaces;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;
using FoodOrderingApplicationFigma.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using FoodOrderingApplicationFigma.Attributes;

namespace FoodOrderingApplicationFigma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IUsers<User> _userRepository;
        private readonly ICustomerService _customerService;
        private readonly IRestaurantService _restaurantService;
        private readonly IAdminService _adminService;
        private readonly IOtpService _otpService;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly IAddressService _addressService;

        public ProfileController(IUsers<User> userRepository, ICustomerService customerService, 
            IRestaurantService restaurantService, IAdminService adminService, IOtpService otpService,
            IEmailService emailService, ISmsService smsService, IAddressService addressService)
        {
            _userRepository = userRepository;
            _customerService = customerService;
            _restaurantService = restaurantService;
            _adminService = adminService;
            _otpService = otpService;
            _emailService = emailService;
            _smsService = smsService;
            _addressService = addressService;
        }
        [AuthorizeRole(4)]
        [HttpPut("customer")]
        public async Task<ActionResult> UpdateCustomerProfile([FromBody] UpdateCustomerProfileDTO dto)
        {
            try
            {
                var user = await _userRepository.GetUserById(dto.UserId);
                if (user == null) throw new Exception("User Not Found");

                if (!string.IsNullOrWhiteSpace(dto.FullName)) user.FullName = dto.FullName;
                if (!string.IsNullOrWhiteSpace(dto.Email)) user.Email = dto.Email;
                if (!string.IsNullOrWhiteSpace(dto.Phone)) user.Phone = dto.Phone;

                if (!string.IsNullOrWhiteSpace(dto.Password))
                {
                    using var hmac = new HMACSHA512();
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
                    user.PasswordSalt = hmac.Key;
                }

                await _userRepository.UpdateUser(user);

                if (!string.IsNullOrWhiteSpace(dto.Address) && dto.CityId.HasValue && dto.StateId.HasValue)
                {
                    var existingAddresses = await _addressService.GetAddressesByUserId(dto.UserId);
                    var defaultAddress = existingAddresses.FirstOrDefault(a => a.IsDefault == true);

                    if (defaultAddress != null)
                    {
                        await _addressService.UpdateAddress(new DTOs.AddressDTOs.UpdateAddressDTO
                        {
                            AddressId = defaultAddress.AddressId,
                            UserId = dto.UserId,
                            AddressLine = dto.Address,
                            CityId = dto.CityId.Value,
                            StateId = dto.StateId.Value,
                            IsDefault = true
                        });
                    }
                    else
                    {
                        await _addressService.CreateAddress(new DTOs.AddressDTOs.CreateAddressDTO
                        {
                            UserId = dto.UserId,
                            AddressLine = dto.Address,
                            CityId = dto.CityId.Value,
                            StateId = dto.StateId.Value,
                            IsDefault = true
                        });
                    }
                }

                return Ok(new { message = "Customer profile updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(2)]
        [HttpPut("restaurant")]
        public async Task<ActionResult> UpdateRestaurantProfile([FromBody] UpdateRestaurantProfileDTO dto)
        {
            try
            {
                var user = await _userRepository.GetUserById(dto.UserId);
                if (user == null) throw new Exception("User Not Found");

                if (!string.IsNullOrWhiteSpace(dto.FullName)) user.FullName = dto.FullName;
                if (!string.IsNullOrWhiteSpace(dto.Email)) user.Email = dto.Email;
                if (!string.IsNullOrWhiteSpace(dto.Phone)) user.Phone = dto.Phone;

                if (!string.IsNullOrWhiteSpace(dto.Password))
                {
                    using var hmac = new HMACSHA512();
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
                    user.PasswordSalt = hmac.Key;
                }

                await _userRepository.UpdateUser(user);

                if (dto.RestaurantId.HasValue)
                {
                    await _restaurantService.UpdateRestaurant(new DTOs.RestaurantDTOs.UpdateRestaurantDTO
                    {
                        RestaurantId = dto.RestaurantId.Value,
                        UserId = dto.UserId,
                        Name = dto.RestaurantName ?? "",
                        Description = dto.Description,
                        Address = dto.Address,
                        CityId = dto.CityId,
                        StateId = dto.StateId
                    });
                }

                return Ok(new { message = "Restaurant profile updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1)]
        [HttpPut("admin")]
        public async Task<ActionResult> UpdateAdminProfile([FromBody] UpdateAdminProfileDTO dto)
        {
            try
            {
                var user = await _userRepository.GetUserById(dto.UserId);
                if (user == null) throw new Exception("User Not Found");

                if (!string.IsNullOrWhiteSpace(dto.FullName)) user.FullName = dto.FullName;
                if (!string.IsNullOrWhiteSpace(dto.Email)) user.Email = dto.Email;
                if (!string.IsNullOrWhiteSpace(dto.Phone)) user.Phone = dto.Phone;

                if (!string.IsNullOrWhiteSpace(dto.Password))
                {
                    using var hmac = new HMACSHA512();
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
                    user.PasswordSalt = hmac.Key;
                }

                await _userRepository.UpdateUser(user);

                return Ok(new { message = "Admin profile updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult> DeleteProfile([FromBody] IdRequestDTO dto)
        {
            try
            {
                var user = await _userRepository.GetUserById(dto.Id);
                if (user == null) throw new Exception("User Not Found");

                await _userRepository.DeleteUser(dto.Id);

                return Ok(new { message = "Profile deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("send-verification-otp")]
        public async Task<ActionResult> SendVerificationOtp([FromQuery] string identifier, [FromQuery] string type)
        {
            try
            {
                var otp = _otpService.GenerateOtp();
                var token = _otpService.GenerateOtpToken(otp, identifier);

                if (type.ToLower() == "email")
                {
                    await _emailService.SendEmailAsync(identifier, "Verify Your Email",
                        $"<h2>Your verification OTP is: <strong>{otp}</strong></h2><p>Valid for 5 minutes.</p>");
                }
                else if (type.ToLower() == "phone")
                {
                    await _smsService.SendOtpAsync(identifier, otp);
                }
                else
                {
                    return BadRequest(new { message = "Type must be 'email' or 'phone'" });
                }

                return Ok(new { token, message = $"OTP sent to {identifier}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("verify-and-update-contact")]
        public async Task<ActionResult> VerifyAndUpdateContact([FromBody] VerifyAndUpdateContactDTO dto)
        {
            try
            {
                var user = await _userRepository.GetUserById(dto.UserId);
                if (user == null) throw new Exception("User Not Found");

                if (!string.IsNullOrWhiteSpace(dto.NewEmail))
                {
                    var isValid = _otpService.ValidateOtpToken(dto.Token, dto.Otp, dto.NewEmail);
                    if (!isValid) return BadRequest(new { message = "Invalid or expired OTP" });
                    
                    user.Email = dto.NewEmail;
                }

                if (!string.IsNullOrWhiteSpace(dto.NewPhone))
                {
                    var isValid = _otpService.ValidateOtpToken(dto.Token, dto.Otp, dto.NewPhone);
                    if (!isValid) return BadRequest(new { message = "Invalid or expired OTP" });
                    
                    user.Phone = dto.NewPhone;
                }

                await _userRepository.UpdateUser(user);

                return Ok(new { message = "Contact information updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
