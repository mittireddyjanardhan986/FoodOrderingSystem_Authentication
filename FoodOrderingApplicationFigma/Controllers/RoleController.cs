using FoodOrderingApplicationFigma.DTOs.RoleDTOs;
using FoodOrderingApplicationFigma.DTOs.CommonDTOs;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FoodOrderingApplicationFigma.Attributes;

namespace FoodOrderingApplicationFigma.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    // [AuthorizeRole(1)]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetRoleDTO>>> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAllRoles();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("get-by-id")]
        public async Task<ActionResult<GetRoleDTO>> GetRoleById([FromBody] IdRequestDTO dto)
        {
            try
            {
                var role = await _roleService.GetRoleById(dto.Id);
                return Ok(role);
            }
            catch (Exception ex) when (ex.Message == "Role Not Found")
            {
                return NotFound(new { message = "Role not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<GetRoleDTO>> CreateRole([FromBody] CreateRoleDTO createRoleDTO)
        {
            try
            {
                var role = await _roleService.CreateRole(createRoleDTO);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<GetRoleDTO>> UpdateRole([FromBody] UpdateRoleDTO updateRoleDTO)
        {
            try
            {
                var role = await _roleService.UpdateRole(updateRoleDTO);
                return Ok(role);
            }
            catch (Exception ex) when (ex.Message == "Role Not Found")
            {
                return NotFound(new { message = "Role not found for update." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<GetRoleDTO>> DeleteRole([FromBody] IdRequestDTO dto)
        {
            try
            {
                var deletedRole = await _roleService.DeleteRole(dto.Id);
                return Ok(deletedRole);
            }
            catch (Exception ex) when (ex.Message == "Role Not Found")
            {
                return NotFound(new { message = "Role not found for deletion." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}