using FoodOrderingApplicationFigma.Data;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Repository.Repository_Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApplicationFigma.Repository.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly FoodOrderingDbContext _context;

        public AddressRepository(FoodOrderingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetAllUsers()
        {
            return await _context.Addresses
                .Include(a => a.City)
                .Include(a => a.State)
                .Include(a => a.User)
                .ToListAsync();
        }

        public async Task<Address?> GetUserById(int id)
        {
            return await _context.Addresses
                .Include(a => a.City)
                .Include(a => a.State)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.AddressId == id);
        }

        public async Task<Address> InsertUser(Address entity)
        {
            _context.Addresses.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Address?> UpdateUser(Address entity)
        {
            var existing = await _context.Addresses.FirstOrDefaultAsync(p=>p.AddressId==entity.AddressId);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null) return false;

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Address>> GetAddressesByUserId(int userId)
        {
            return await _context.Addresses
                .Include(a => a.City)
                .Include(a => a.State)
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public Task<Address?> GetUserByEmailOrPhone(string emailOrPhone)
        {
            throw new NotImplementedException();
        }
    }
}