using FoodOrderingApplicationFigma.Interfaces;
using FoodOrderingApplicationFigma.Models;

namespace FoodOrderingApplicationFigma.Repository.Repository_Interface
{
    public interface IAddressRepository : IUsers<Address>
    {
        Task<IEnumerable<Address>> GetAddressesByUserId(int userId);
    }
}