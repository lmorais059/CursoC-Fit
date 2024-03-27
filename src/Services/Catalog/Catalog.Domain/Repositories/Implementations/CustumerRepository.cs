using Catalog.Domain.Repositories.Interfaces;
using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Domain.Repositories.Implementations
{
    internal sealed class CustomerRepository : ICustomerRepository
    {

        private DbContext Database { get; }

        public CustomerRepository(DbContext database)
        {
            Database = database;
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            await Database.AddAsync(customer); // Create Customer
            await Database.SaveChangesAsync(); // Save Customer

            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await Database.Set<Customer>().ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await Database.Set<Customer>().FirstOrDefaultAsync(customer => customer.Id == id);
        }

        public async Task UpdateAsync(Customer customer)
        {
            Database.Update(customer);
            await Database.SaveChangesAsync();
        }
    }
}