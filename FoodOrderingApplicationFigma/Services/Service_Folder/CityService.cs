using AutoMapper;
using FoodOrderingApplicationFigma.DTOs.CityDTOs;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Repository.Repository_Interface;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;

namespace FoodOrderingApplicationFigma.Services.Service_Folder
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetCityDTO>> GetAllCities()
        {
            var cities = await _cityRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<GetCityDTO>>(cities);
        }

        public async Task<GetCityDTO?> GetCityById(int id)
        {
            var city = await _cityRepository.GetUserById(id);
            if (city == null) throw new Exception("City Not Found");
            return _mapper.Map<GetCityDTO>(city);
        }

        public async Task<GetCityDTO> CreateCity(CreateCityDTO createCityDTO)
        {
            var city = _mapper.Map<City>(createCityDTO);
            var createdCity = await _cityRepository.InsertUser(city);
            return _mapper.Map<GetCityDTO>(createdCity);
        }

        public async Task<GetCityDTO?> UpdateCity(UpdateCityDTO updateCityDTO)
        {
            var existing = await _cityRepository.GetUserById(updateCityDTO.CityId);
            if (existing == null) throw new Exception("City Not Found");
            
            var city = _mapper.Map<City>(updateCityDTO);
            var updatedCity = await _cityRepository.UpdateUser(city);
            return _mapper.Map<GetCityDTO>(updatedCity);
        }

        public async Task<GetCityDTO> DeleteCity(int id)
        {
            var city = await _cityRepository.GetUserById(id);
            if (city == null) throw new Exception("City Not Found");
            
            await _cityRepository.DeleteUser(id);
            return _mapper.Map<GetCityDTO>(city);
        }

        public async Task<IEnumerable<GetCityDTO>> GetCitiesByStateId(int stateId)
        {
            var cities = await _cityRepository.GetCitiesByStateId(stateId);
            return _mapper.Map<IEnumerable<GetCityDTO>>(cities);
        }
    }
}