using FoodOrderingApplicationFigma.Models;

namespace FoodOrderingApplicationFigma.Repository.Repository_Interface
{
    public interface ICustomerRepositorySpecific
    {
        Task<IEnumerable<Customer>> GetAllUsers();
        Task<Customer?> GetUserById(long id);
        Task<Customer> InsertUser(Customer entity);
        Task<Customer?> UpdateUser(Customer entity);
        Task<bool> DeleteUser(long id);
        Task<Customer?> GetUserByEmailOrPhone(string emailOrPhone);
    }
}