using FoodOrderingApplicationFigma.DTOs.StateDTOs;
using FoodOrderingApplicationFigma.DTOs.CommonDTOs;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FoodOrderingApplicationFigma.Attributes;

namespace FoodOrderingApplicationFigma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<GetStateDTO>>> GetAllStates()
        {
            try
            {
                var states = await _stateService.GetAllStates();
                return Ok(states);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPost("get-by-id")]
        public async Task<ActionResult<GetStateDTO>> GetStateById([FromBody] IdRequestDTO dto)
        {
            try
            {
                var state = await _stateService.GetStateById(dto.Id);
                return Ok(state);
            }
            catch (Exception ex) when (ex.Message == "State Not Found")
            {
                return NotFound(new { message = "State not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1)]
        [HttpPost]
        public async Task<ActionResult<GetStateDTO>> CreateState([FromBody] CreateStateDTO createStateDTO)
        {
            try
            {
                var state = await _stateService.CreateState(createStateDTO);
                return Ok(state);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1)]
        [HttpPut]
        public async Task<ActionResult<GetStateDTO>> UpdateState([FromBody] UpdateStateDTO updateStateDTO)
        {
            try
            {
                var state = await _stateService.UpdateState(updateStateDTO);
                return Ok(state);
            }
            catch (Exception ex) when (ex.Message == "State Not Found")
            {
                return NotFound(new { message = "State not found for update." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1)]
        [HttpPost("delete")]
        public async Task<ActionResult<GetStateDTO>> DeleteState([FromBody] IdRequestDTO dto)
        {
            try
            {
                var deletedState = await _stateService.DeleteState(dto.Id);
                return Ok(deletedState);
            }
            catch (Exception ex) when (ex.Message == "State Not Found")
            {
                return NotFound(new { message = "State not found for deletion." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}