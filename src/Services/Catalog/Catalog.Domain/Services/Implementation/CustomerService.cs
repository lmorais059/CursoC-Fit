using System.Net;
using Catalog.Domain.Data;
using Catalog.Domain.Errors;
using Catalog.Domain.Models;
using Catalog.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Domain.Services.Implementations
{
    internal sealed class CustomerService : ICustomerService
    {
        private DatabaseContext Database { get; }

        public CustomerService(DatabaseContext databaseContext)
        {
            Database = databaseContext;
        }

        public async Task<Customer> CreateAsync(string name, string email)
        {
            Customer customer = Customer.Create(name, email);

            await Database.AddAsync(customer);
            await Database.SaveChangesAsync();

            return customer;
        }
        public async Task UpdateAsync(Guid id, string name, string email)
        {
            Customer? customer = await Database.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if (customer is null)
            {
                throw new AppError("Customer not found", HttpStatusCode.NotFound);
            }

            customer.Update(name, email);

            Database.Update(customer);
            await Database.SaveChangesAsync();
        }
    }
}