using FoodOrderingApplicationFigma.Data;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Repository.Repository_Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApplicationFigma.Repository.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly FoodOrderingDbContext _context;

        public RoleRepository(FoodOrderingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllUsers()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetUserById(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
        }

        public async Task<Role> InsertUser(Role entity)
        {
            _context.Roles.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Role?> UpdateUser(Role entity)
        {
            var existing = await _context.Roles.FindAsync(entity.RoleId);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return false;

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Role?> GetUserByEmailOrPhone(string emailOrPhone)
        {
            return null; // Roles don't have email/phone
        }
    }
}