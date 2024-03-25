using Catalog.Domain.Models;
using Catalog.Domain.Services.Interfaces;

namespace Catalog.Domain.Services.Implementation
{
    internal sealed class CustomerService : ICustomerService
    {
        // Your code here
        public Task<Customer> CreateAsync(string nome, string email)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Dado inválido");
            }

            Customer customer = new()
            {
                Name = nome,
                Email = email
            };

            return Task.FromResult(customer);
        }

        public Task UpdateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}