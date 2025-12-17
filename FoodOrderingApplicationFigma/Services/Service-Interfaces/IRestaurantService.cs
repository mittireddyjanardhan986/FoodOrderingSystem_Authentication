using FoodOrderingApplicationFigma.DTOs.RestaurantDTOs;

namespace FoodOrderingApplicationFigma.Services.Service_Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<GetRestaurantDTO>> GetAllRestaurants();
        Task<GetRestaurantDTO?> GetRestaurantById(int id);
        Task<GetRestaurantDTO> CreateRestaurant(CreateRestaurantDTO createRestaurantDTO);
        Task<GetRestaurantDTO?> UpdateRestaurant(UpdateRestaurantDTO updateRestaurantDTO);
        Task<GetRestaurantDTO> DeleteRestaurant(int id);
        Task<IEnumerable<GetRestaurantDTO>> GetRestaurantsByCityId(int cityId);
        Task<GetRestaurantDTO?> GetRestaurantByUserId(int userId);
    }
}