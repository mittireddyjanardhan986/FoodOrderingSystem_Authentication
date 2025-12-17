using FoodOrderingApplicationFigma.Data;
using FoodOrderingApplicationFigma.Interfaces;
using FoodOrderingApplicationFigma.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApplicationFigma.Repository
{
    public class UserRepository : IUsers<User>
    {
        private readonly FoodOrderingDbContext _context;

        public UserRepository(FoodOrderingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users
                .Include(u => u.Addresses)
                    .ThenInclude(a => a.City)
                .Include(u => u.Addresses)
                    .ThenInclude(a => a.State)
                .Include(u => u.Carts)
                    .ThenInclude(c => c.CartItems)
                        .ThenInclude(ci => ci.MenuItem)
                .Include(u => u.Orders)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.MenuItem)
                .Include(u => u.RoleNavigation)
                .ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users
                .Include(u => u.Addresses)
                    .ThenInclude(a => a.City)
                .Include(u => u.Addresses)
                    .ThenInclude(a => a.State)
                .Include(u => u.Carts)
                    .ThenInclude(c => c.CartItems)
                        .ThenInclude(ci => ci.MenuItem)
                .Include(u => u.Orders)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.MenuItem)
                .Include(u => u.RoleNavigation)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> InsertUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            var existing = await _context.Users.FindAsync(user.UserId);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetUserByEmailOrPhone(string emailOrPhone)
        {
            return await _context.Users
                .Include(u => u.Addresses)
                    .ThenInclude(a => a.City)
                .Include(u => u.Addresses)
                    .ThenInclude(a => a.State)
                .Include(u => u.RoleNavigation)
                .FirstOrDefaultAsync(u => u.Email == emailOrPhone || u.Phone == emailOrPhone);
        }
    }
}
