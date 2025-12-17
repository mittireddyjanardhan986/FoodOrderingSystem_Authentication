using FoodOrderingApplicationFigma.Data;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Repository.Repository_Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApplicationFigma.Repository.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly FoodOrderingDbContext _context;

        public StateRepository(FoodOrderingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<State>> GetAllUsers()
        {
            return await _context.States.ToListAsync();
        }

        public async Task<State?> GetUserById(int id)
        {
            return await _context.States.FirstOrDefaultAsync(s => s.StateId == id);
        }

        public async Task<State> InsertUser(State entity)
        {
            _context.States.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<State?> UpdateUser(State entity)
        {
            var existing = await _context.States.FindAsync(entity.StateId);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var state = await _context.States.FindAsync(id);
            if (state == null) return false;

            _context.States.Remove(state);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<State?> GetUserByEmailOrPhone(string emailOrPhone)
        {
            return null; // States don't have email/phone
        }
    }
}