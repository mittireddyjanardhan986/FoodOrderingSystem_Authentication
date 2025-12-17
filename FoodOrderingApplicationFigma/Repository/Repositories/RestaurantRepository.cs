using FoodOrderingApplicationFigma.Data;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Repository.Repository_Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApplicationFigma.Repository.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly FoodOrderingDbContext _context;

        public RestaurantRepository(FoodOrderingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Restaurant>> GetAllUsers()
        {
            return await _context.Restaurants
                .Include(r => r.User)
                .Include(r => r.City)
                .Include(r => r.State)
                .ToListAsync();
        }

        public async Task<Restaurant?> GetUserById(int id)
        {
            return await _context.Restaurants
                .Include(r => r.User)
                .Include(r => r.City)
                .Include(r => r.State)
                .FirstOrDefaultAsync(r => r.RestaurantId == id);
        }

        public async Task<Restaurant> InsertUser(Restaurant entity)
        {
            _context.Restaurants.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Restaurant?> UpdateUser(Restaurant entity)
        {
            var existing = await _context.Restaurants.FindAsync(entity.RestaurantId);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return false;

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurantsByCityId(int cityId)
        {
            return await _context.Restaurants
                .Include(r => r.User)
                .Include(r => r.City)
                .Include(r => r.State)
                .Where(r => r.CityId == cityId)
                .ToListAsync();
        }

        public async Task<Restaurant?> GetUserByEmailOrPhone(string emailOrPhone)
        {
            return await _context.Restaurants
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.User.Email == emailOrPhone || r.User.Phone == emailOrPhone);
        }
    }
}