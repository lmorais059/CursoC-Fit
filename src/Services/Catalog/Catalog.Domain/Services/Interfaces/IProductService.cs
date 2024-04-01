using Catalog.Domain.Models;

namespace Catalog.Domain.Services.Intefaces
{
    public interface IProductService
    {
        Task<Product> CreateAsync(Guid customerId, string name, decimal price);
        Task UpdateAsync(Guid id, string name, decimal price);
        Task DeleteAsync(Guid id);
    }
}