using AutoMapper;
using FoodOrderingApplicationFigma.DTOs.AddressDTOs;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Repository.Repository_Interface;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;

namespace FoodOrderingApplicationFigma.Services.Service_Folder
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAddressDTO>> GetAllAddresses()
        {
            var addresses = await _addressRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<GetAddressDTO>>(addresses);
        }

        public async Task<GetAddressDTO?> GetAddressById(int id)
        {
            var address = await _addressRepository.GetUserById(id);
            if (address == null) throw new Exception("Address Not Found");
            return _mapper.Map<GetAddressDTO>(address);
        }

        public async Task<GetAddressDTO> CreateAddress(CreateAddressDTO createAddressDTO)
        {
            var address = _mapper.Map<Address>(createAddressDTO);
            var createdAddress = await _addressRepository.InsertUser(address);
            return _mapper.Map<GetAddressDTO>(createdAddress);
        }

        public async Task<GetAddressDTO?> UpdateAddress(UpdateAddressDTO updateAddressDTO)
        {
            var existing = await _addressRepository.GetUserById(updateAddressDTO.AddressId);
            if (existing == null) throw new Exception("Address Not Found");
            
            var address = _mapper.Map<Address>(updateAddressDTO);
            var updatedAddress = await _addressRepository.UpdateUser(address);
            return _mapper.Map<GetAddressDTO>(updatedAddress);
        }

        public async Task<GetAddressDTO> DeleteAddress(int id)
        {
            var address = await _addressRepository.GetUserById(id);
            if (address == null) throw new Exception("Address Not Found");
            
            await _addressRepository.DeleteUser(id);
            return _mapper.Map<GetAddressDTO>(address);
        }

        public async Task<IEnumerable<GetAddressDTO>> GetAddressesByUserId(int userId)
        {
            var addresses = await _addressRepository.GetAddressesByUserId(userId);
            return _mapper.Map<IEnumerable<GetAddressDTO>>(addresses);
        }
    }
}