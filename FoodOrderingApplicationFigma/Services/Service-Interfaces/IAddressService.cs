using FoodOrderingApplicationFigma.DTOs.AddressDTOs;

namespace FoodOrderingApplicationFigma.Services.Service_Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<GetAddressDTO>> GetAllAddresses();
        Task<GetAddressDTO?> GetAddressById(int id);
        Task<GetAddressDTO> CreateAddress(CreateAddressDTO createAddressDTO);
        Task<GetAddressDTO?> UpdateAddress(UpdateAddressDTO updateAddressDTO);
        Task<GetAddressDTO> DeleteAddress(int id);
        Task<IEnumerable<GetAddressDTO>> GetAddressesByUserId(int userId);
    }
}