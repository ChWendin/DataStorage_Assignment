

using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CustomerRepository(DataContext context)
    {
        private readonly DataContext _context = context;

        // C
        public async Task<CustomerEntity> CreateAsync(CustomerEntity customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        // R
        public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        // U
        public async Task<CustomerEntity?> UpdateAsync(CustomerEntity customer)
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.CustomerName = customer.CustomerName;
                await _context.SaveChangesAsync();
                return existingCustomer;
            }
            return null;
               
        }

        // D
        public async Task<bool> DeleteAsync(int id) 
        {
            var entity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null) 
            {
                _context.Customers.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
