using Catalog.Domain.Repositorires.Interfaces;
using Catalog.Domain.Models;


        ###Comentários pragma teste


namespace Catalog.Domain.Repositorires.Implementations{

    internal sealed class CustomerRepository : ICustomerRepository
    {
        public Task<Customer> CreateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}

