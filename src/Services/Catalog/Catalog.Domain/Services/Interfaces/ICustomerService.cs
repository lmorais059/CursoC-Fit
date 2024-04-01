using Catalog.Domain.Models;

namespace Catalog.Domain.Services.Intefaces
{
    public interface ICustomerService
    {
        Task<Customer> CreateAsync(string name, string email);
        Task UpdateAsync(Guid id, string name, string email);
    }
}