using FoodOrderingApplicationFigma.DTOs;
using FoodOrderingApplicationFigma.DTOs.CommonDTOs;
using FoodOrderingApplicationFigma.Interfaces;
using FoodOrderingApplicationFigma.Services;
using FoodOrderingApplicationFigma.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingApplicationFigma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet]
        [AuthorizeRole(1)]
        public async Task<ActionResult<IEnumerable<GetAllUserDTO>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1, 4, 2)]
        [HttpPost("get-by-id")]
        public async Task<ActionResult<GetAllUserDTO>> GetUserById([FromBody] IdRequestDTO dto)
        {
            try
            {
                var user = await _userService.GetUserById(dto.Id);
                return Ok(user);
            }
            catch (Exception ex) when (ex.Message == "User Not Found")
            {
                return NotFound(new { message = "User not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1, 4, 2)]
        [HttpPost]
        public async Task<ActionResult<GetAllUserDTO>> CreateUser([FromBody] CreateUserDTO dto)
        {
            try
            {
                var createdUser = await _userService.InsertUser(dto);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1, 4, 2)]
        [HttpPut]
        public async Task<ActionResult<GetAllUserDTO>> UpdateUser([FromBody] UpdateUserDTO dto)
        {
            try
            {
                var updatedUser = await _userService.UpdateUser(dto);
                return Ok(updatedUser);
            }
            catch (Exception ex) when (ex.Message == "User Not Found")
            {
                return NotFound(new { message = "User not found for update." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<GetAllUserDTO>> DeleteUser([FromBody] IdRequestDTO dto)
        {
            try
            {
                var deletedUser = await _userService.DeleteUser(dto.Id);
                return Ok(deletedUser);
            }
            catch (Exception ex) when (ex.Message == "User Not Found")
            {
                return NotFound(new { message = "User not found for deletion." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1, 4, 2)]
        [HttpPost("get-by-email-or-phone")]
        public async Task<ActionResult<GetAllUserDTO>> GetUserByEmailOrPhone([FromBody] EmailOrPhoneRequestDTO dto)
        {
            try
            {
                var user = await _userService.GetUserByEmailOrPhone(dto.EmailOrPhone);
                return Ok(user);
            }
            catch (Exception ex) when (ex.Message == "User Not Found")
            {
                return NotFound(new { message = "User not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
