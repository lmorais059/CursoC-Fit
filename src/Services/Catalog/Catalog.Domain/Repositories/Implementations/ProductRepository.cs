using Catalog.Domain.Models;
using Catalog.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Domain.Repositories.Implementations
{
    internal sealed class ProductRepository : IProductRepository
    {

        private DbContext Database { get; }

        public ProductRepository(DbContext database)
        {
            Database = database;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await Database.AddAsync(product);
            await Database.SaveChangesAsync();

            return product;
        }

        public async Task DeleteAsync(Guid id)
        {
            Product? product = await Database.Set<Product>().FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                throw new InvalidOperationException("Product not found");
            }

            Database.Remove(product);
            await Database.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await Database.Set<Product>().ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await Database.Set<Product>().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Product product)
        {
            Database.Update(product);
            await Database.SaveChangesAsync();
        }
    }
}