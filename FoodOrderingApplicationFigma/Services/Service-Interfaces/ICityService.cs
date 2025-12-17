using FoodOrderingApplicationFigma.DTOs.CityDTOs;

namespace FoodOrderingApplicationFigma.Services.Service_Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<GetCityDTO>> GetAllCities();
        Task<GetCityDTO?> GetCityById(int id);
        Task<GetCityDTO> CreateCity(CreateCityDTO createCityDTO);
        Task<GetCityDTO?> UpdateCity(UpdateCityDTO updateCityDTO);
        Task<GetCityDTO> DeleteCity(int id);
        Task<IEnumerable<GetCityDTO>> GetCitiesByStateId(int stateId);
    }
}