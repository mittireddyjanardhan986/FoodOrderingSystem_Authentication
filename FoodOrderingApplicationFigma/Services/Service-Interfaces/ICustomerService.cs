using FoodOrderingApplicationFigma.DTOs.CustomerDTOs;

namespace FoodOrderingApplicationFigma.Services.Service_Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<GetCustomerDTO>> GetAllCustomers();
        Task<GetCustomerDTO?> GetCustomerById(int id);
        Task<GetCustomerDTO> CreateCustomer(CreateCustomerDTO createCustomerDTO);
        Task<GetCustomerDTO?> UpdateCustomer(UpdateCustomerDTO updateCustomerDTO);
        Task<GetCustomerDTO> DeleteCustomer(int id);
    }
}