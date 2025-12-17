using FoodOrderingApplicationFigma.DTOs.RestaurantDTOs;
using FoodOrderingApplicationFigma.DTOs.CommonDTOs;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FoodOrderingApplicationFigma.Attributes;

namespace FoodOrderingApplicationFigma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        [AuthorizeRole(1)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetRestaurantDTO>>> GetAllRestaurants()
        {
            try
            {
                var restaurants = await _restaurantService.GetAllRestaurants();
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1,2,4)]
        [HttpPost("get-by-id")]
        public async Task<ActionResult<GetRestaurantDTO>> GetRestaurantById([FromBody] IdRequestDTO dto)
        {
            try
            {
                var restaurant = await _restaurantService.GetRestaurantById(dto.Id);
                return Ok(restaurant);
            }
            catch (Exception ex) when (ex.Message == "Restaurant Not Found")
            {
                return NotFound(new { message = "Restaurant not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1,2,4)]
        [HttpPut]
        public async Task<ActionResult<GetRestaurantDTO>> UpdateRestaurant([FromBody] UpdateRestaurantDTO updateRestaurantDTO)
        {
            try
            {
                var restaurant = await _restaurantService.UpdateRestaurant(updateRestaurantDTO);
                return Ok(restaurant);
            }
            catch (Exception ex) when (ex.Message == "Restaurant Not Found")
            {
                return NotFound(new { message = "Restaurant not found for update." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1)]
        [HttpPost("delete")]
        public async Task<ActionResult<GetRestaurantDTO>> DeleteRestaurant([FromBody] IdRequestDTO dto)
        {
            try
            {
                var deletedRestaurant = await _restaurantService.DeleteRestaurant(dto.Id);
                return Ok(deletedRestaurant);
            }
            catch (Exception ex) when (ex.Message == "Restaurant Not Found")
            {
                return NotFound(new { message = "Restaurant not found for deletion." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1,2,4)]
        [HttpPost("get-by-city")]
        public async Task<ActionResult<IEnumerable<GetRestaurantDTO>>> GetRestaurantsByCityId([FromBody] IdRequestDTO dto)
        {
            try
            {
                var restaurants = await _restaurantService.GetRestaurantsByCityId(dto.Id);
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPost("get-by-user-id")]
        public async Task<ActionResult<GetRestaurantDTO>> GetRestaurantByUserId([FromBody] IdRequestDTO dto)
        {
            try
            {
                var restaurant = await _restaurantService.GetRestaurantByUserId(dto.Id);
                return Ok(restaurant);
            }
            catch (Exception ex) when (ex.Message == "Restaurant Not Found")
            {
                return NotFound(new { message = "Restaurant not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}