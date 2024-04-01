using System.Net;
using Catalog.Domain.Data;
using Catalog.Domain.Errors;
using Catalog.Domain.Models;
using Catalog.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Domain.Services.Implementations
{
    internal sealed class ProductService : IProductService
    {

        private DatabaseContext Database { get; }

        public ProductService(DatabaseContext database)
        {
            Database = database;
        }

        public async Task<Product> CreateAsync(Guid customerId, string name, decimal price)
        {
            // Encontrar o Customer pelo Id
            Customer? customer = await Database.Customers.FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer is null)
            {
                throw new AppError("Customer not found", HttpStatusCode.NotFound);
            }

            // Criar o Produto
            Product product = Product.Create(customer, name, price);

            // Salvar no Banco
            await Database.AddAsync(product);
            await Database.SaveChangesAsync();

            return product;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Guid id, string name, decimal price)
        {
            Product? product = await Database.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                throw new AppError("Product not found", HttpStatusCode.NotFound);
            }

            product.Update(name, price);

            Database.Update(product);
            await Database.SaveChangesAsync();
        }
    }
}