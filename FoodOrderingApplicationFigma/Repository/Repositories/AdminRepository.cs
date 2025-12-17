using FoodOrderingApplicationFigma.Data;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Repository.Repository_Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApplicationFigma.Repository.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly FoodOrderingDbContext _context;

        public AdminRepository(FoodOrderingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Admin>> GetAllUsers()
        {
            return await _context.Admins
                .Include(a => a.User)
                .ToListAsync();
        }

        public async Task<Admin?> GetUserById(int id)
        {
            return await _context.Admins
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.AdminId == id);
        }

        public async Task<Admin> InsertUser(Admin entity)
        {
            _context.Admins.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Admin?> UpdateUser(Admin entity)
        {
            var existing = await _context.Admins.FindAsync(entity.AdminId);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null) return false;

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Admin?> GetUserByEmailOrPhone(string emailOrPhone)
        {
            return await _context.Admins
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.User.Email == emailOrPhone || a.User.Phone == emailOrPhone);
        }
    }
}