using FoodOrderingApplicationFigma.Attributes;
using FoodOrderingApplicationFigma.DTOs.CityDTOs;
using FoodOrderingApplicationFigma.DTOs.CommonDTOs;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingApplicationFigma.Controllers
{
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<GetCityDTO>>> GetAllCities()
        {
            try
            {
                var cities = await _cityService.GetAllCities();
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPost("get-by-id")]
        public async Task<ActionResult<GetCityDTO>> GetCityById([FromBody] IdRequestDTO dto)
        {
            try
            {
                var city = await _cityService.GetCityById(dto.Id);
                return Ok(city);
            }
            catch (Exception ex) when (ex.Message == "City Not Found")
            {
                return NotFound(new { message = "City not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1)]
        [HttpPost]
        public async Task<ActionResult<GetCityDTO>> CreateCity([FromBody] CreateCityDTO createCityDTO)
        {
            try
            {
                var city = await _cityService.CreateCity(createCityDTO);
                return Ok(city);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1)]
        [HttpPut]
        public async Task<ActionResult<GetCityDTO>> UpdateCity([FromBody] UpdateCityDTO updateCityDTO)
        {
            try
            {
                var city = await _cityService.UpdateCity(updateCityDTO);
                return Ok(city);
            }
            catch (Exception ex) when (ex.Message == "City Not Found")
            {
                return NotFound(new { message = "City not found for update." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1)]
        [HttpPost("delete")]
        public async Task<ActionResult<GetCityDTO>> DeleteCity([FromBody] IdRequestDTO dto)
        {
            try
            {
                var deletedCity = await _cityService.DeleteCity(dto.Id);
                return Ok(deletedCity);
            }
            catch (Exception ex) when (ex.Message == "City Not Found")
            {
                return NotFound(new { message = "City not found for deletion." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPost("get-by-state")]
        public async Task<ActionResult<IEnumerable<GetCityDTO>>> GetCitiesByStateId([FromBody] IdRequestDTO dto)
        {
            try
            {
                var cities = await _cityService.GetCitiesByStateId(dto.Id);
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}