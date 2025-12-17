using AutoMapper;
using FoodOrderingApplicationFigma.DTOs.CustomerDTOs;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Repository.Repository_Interface;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;

namespace FoodOrderingApplicationFigma.Services.Service_Folder
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetCustomerDTO>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<GetCustomerDTO>>(customers);
        }

        public async Task<GetCustomerDTO?> GetCustomerById(long id)
        {
            var customer = await _customerRepository.GetUserById(id);
            if (customer == null) throw new Exception("Customer Not Found");
            return _mapper.Map<GetCustomerDTO>(customer);
        }

        public async Task<GetCustomerDTO> CreateCustomer(CreateCustomerDTO createCustomerDTO)
        {
            var customer = _mapper.Map<Customer>(createCustomerDTO);
            var createdCustomer = await _customerRepository.InsertUser(customer);
            return _mapper.Map<GetCustomerDTO>(createdCustomer);
        }

        public async Task<GetCustomerDTO?> UpdateCustomer(UpdateCustomerDTO updateCustomerDTO)
        {
            var existing = await _customerRepository.GetUserById(updateCustomerDTO.CustomerId);
            if (existing == null) throw new Exception("Customer Not Found");
            
            var customer = _mapper.Map<Customer>(updateCustomerDTO);
            var updatedCustomer = await _customerRepository.UpdateUser(customer);
            return _mapper.Map<GetCustomerDTO>(updatedCustomer);
        }

        public async Task<GetCustomerDTO> DeleteCustomer(long id)
        {
            var customer = await _customerRepository.GetUserById(id);
            if (customer == null) throw new Exception("Customer Not Found");
            
            await _customerRepository.DeleteUser(id);
            return _mapper.Map<GetCustomerDTO>(customer);
        }
    }
}