using FoodOrderingApplicationFigma.Interfaces;
using FoodOrderingApplicationFigma.Models;

namespace FoodOrderingApplicationFigma.Repository.Repository_Interface
{
    public interface ICityRepository : IUsers<City>
    {
        Task<IEnumerable<City>> GetCitiesByStateId(int stateId);
    }
}