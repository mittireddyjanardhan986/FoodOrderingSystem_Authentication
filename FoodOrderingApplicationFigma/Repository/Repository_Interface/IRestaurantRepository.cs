using FoodOrderingApplicationFigma.Interfaces;
using FoodOrderingApplicationFigma.Models;

namespace FoodOrderingApplicationFigma.Repository.Repository_Interface
{
    public interface IRestaurantRepository : IUsers<Restaurant>
    {
        Task<IEnumerable<Restaurant>> GetRestaurantsByCityId(int cityId);
    }
}