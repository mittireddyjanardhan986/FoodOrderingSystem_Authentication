using FoodOrderingApplicationFigma.Data;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Repository.Repository_Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApplicationFigma.Repository.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FoodOrderingDbContext _context;

        public CustomerRepository(FoodOrderingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllUsers()
        {
            return await _context.Customers
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task<Customer?> GetUserById(int id)
        {
            return await _context.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> InsertUser(Customer entity)
        {
            _context.Customers.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Customer?> UpdateUser(Customer entity)
        {
            var existing = await _context.Customers.FindAsync(entity.CustomerId);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Customer?> GetUserByEmailOrPhone(string emailOrPhone)
        {
            return await _context.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.User.Email == emailOrPhone || c.User.Phone == emailOrPhone);
        }
    }
}