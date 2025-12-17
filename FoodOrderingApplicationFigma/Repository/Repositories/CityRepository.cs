using FoodOrderingApplicationFigma.Data;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Repository.Repository_Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApplicationFigma.Repository.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly FoodOrderingDbContext _context;

        public CityRepository(FoodOrderingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetAllUsers()
        {
            return await _context.Cities
                .Include(c => c.State)
                .ToListAsync();
        }

        public async Task<City?> GetUserById(int id)
        {
            return await _context.Cities
                .Include(c => c.State)
                .FirstOrDefaultAsync(c => c.CityId == id);
        }

        public async Task<City> InsertUser(City entity)
        {
            _context.Cities.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<City?> UpdateUser(City entity)
        {
            var existing = await _context.Cities.FindAsync(entity.CityId);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null) return false;

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<City>> GetCitiesByStateId(int stateId)
        {
            return await _context.Cities
                .Include(c => c.State)
                .Where(c => c.StateId == stateId)
                .ToListAsync();
        }

        public async Task<City?> GetUserByEmailOrPhone(string emailOrPhone)
        {
            return null; // Cities don't have email/phone
        }
    }
}