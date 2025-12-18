using AutoMapper;
using FoodOrderingApplicationFigma.DTOs.RestaurantDTOs;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Repository.Repository_Interface;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;

namespace FoodOrderingApplicationFigma.Services.Service_Folder
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetRestaurantDTO>> GetAllRestaurants()
        {
            var restaurants = await _restaurantRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<GetRestaurantDTO>>(restaurants);
        }

        public async Task<GetRestaurantDTO?> GetRestaurantById(int id)
        {
            var restaurant = await _restaurantRepository.GetUserById(id);
            if (restaurant == null) throw new Exception("Restaurant Not Found");
            return _mapper.Map<GetRestaurantDTO>(restaurant);
        }

        public async Task<GetRestaurantDTO> CreateRestaurant(CreateRestaurantDTO createRestaurantDTO)
        {
            var restaurant = _mapper.Map<Restaurant>(createRestaurantDTO);
            var createdRestaurant = await _restaurantRepository.InsertUser(restaurant);
            return _mapper.Map<GetRestaurantDTO>(createdRestaurant);
        }

        public async Task<GetRestaurantDTO?> UpdateRestaurant(UpdateRestaurantDTO updateRestaurantDTO)
        {
            var existing = await _restaurantRepository.GetUserById(updateRestaurantDTO.RestaurantId);
            if (existing == null) throw new Exception("Restaurant Not Found");
            
            var restaurant = _mapper.Map<Restaurant>(updateRestaurantDTO);
            var updatedRestaurant = await _restaurantRepository.UpdateUser(restaurant);
            return _mapper.Map<GetRestaurantDTO>(updatedRestaurant);
        }

        public async Task<GetRestaurantDTO> DeleteRestaurant(int id)
        {
            var restaurant = await _restaurantRepository.GetUserById(id);
            if (restaurant == null) throw new Exception("Restaurant Not Found");
            
            await _restaurantRepository.DeleteUser(id);
            return _mapper.Map<GetRestaurantDTO>(restaurant);
        }

        public async Task<IEnumerable<GetRestaurantDTO>> GetRestaurantsByCityId(int cityId)
        {
            var restaurants = await _restaurantRepository.GetRestaurantsByCityId(cityId);
            return _mapper.Map<IEnumerable<GetRestaurantDTO>>(restaurants);
        }

        public async Task<GetRestaurantDTO?> GetRestaurantByUserId(int userId)
        {
            var restaurants = await _restaurantRepository.GetAllUsers();
            var restaurant = restaurants.FirstOrDefault(r => r.UserId == userId);
            return restaurant != null ? _mapper.Map<GetRestaurantDTO>(restaurant) : null;
        }
    }
}