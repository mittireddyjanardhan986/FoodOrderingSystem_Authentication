using FoodOrderingApplicationFigma.DTOs.CustomerDTOs;

namespace FoodOrderingApplicationFigma.Services.Service_Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<GetCustomerDTO>> GetAllCustomers();
        Task<GetCustomerDTO?> GetCustomerById(long id);
        Task<GetCustomerDTO> CreateCustomer(CreateCustomerDTO createCustomerDTO);
        Task<GetCustomerDTO?> UpdateCustomer(UpdateCustomerDTO updateCustomerDTO);
        Task<GetCustomerDTO> DeleteCustomer(long id);
    }
}