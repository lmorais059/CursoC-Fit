using Catalog.Domain.Models;

namespace Catalog.Domain.Repositorires.Interfaces
{
    public interface ICustomerRepository{
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(Guid id);
        Task<Customer> CreateAsync(Customer customer);
        Task UpdateAsync(Customer customer);
    }

}