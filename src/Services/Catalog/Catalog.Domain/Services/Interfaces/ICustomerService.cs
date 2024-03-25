using Catalog.Domain.Models;


namespace Catalog.Domain.Services.Interfaces{

    public interface ICustomerService{
        Task<Customer> CreateAsync(string nome, string email);
        Task UpdateAsync( Guid id,string nome, string email);
        

    }

}