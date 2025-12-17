using FoodOrderingApplicationFigma.Attributes;
using FoodOrderingApplicationFigma.DTOs.AdminDTOs;
using FoodOrderingApplicationFigma.DTOs.CommonDTOs;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingApplicationFigma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeRole(2)]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAdminDTO>>> GetAllAdmins()
        {
            try
            {
                var admins = await _adminService.GetAllAdmins();
                return Ok(admins);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("get-by-id")]
        public async Task<ActionResult<GetAdminDTO>> GetAdminById([FromBody] IdRequestDTO dto)
        {
            try
            {
                var admin = await _adminService.GetAdminById(dto.Id);
                return Ok(admin);
            }
            catch (Exception ex) when (ex.Message == "Admin Not Found")
            {
                return NotFound(new { message = "Admin not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<GetAdminDTO>> UpdateAdmin([FromBody] UpdateAdminDTO updateAdminDTO)
        {
            try
            {
                var admin = await _adminService.UpdateAdmin(updateAdminDTO);
                return Ok(admin);
            }
            catch (Exception ex) when (ex.Message == "Admin Not Found")
            {
                return NotFound(new { message = "Admin not found for update." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<GetAdminDTO>> DeleteAdmin([FromBody] IdRequestDTO dto)
        {
            try
            {
                var deletedAdmin = await _adminService.DeleteAdmin(dto.Id);
                return Ok(deletedAdmin);
            }
            catch (Exception ex) when (ex.Message == "Admin Not Found")
            {
                return NotFound(new { message = "Admin not found for deletion." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}