using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Domain.Data
{
    public sealed class DatabaseContext : DbContext, IDataProvider
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }


        public IQueryable<Customer> Customers => Set<Customer>();
        public IQueryable<Product> Products => Set<Product>();
    }

    public interface IDataProvider
    {
        IQueryable<Customer> Customers { get; }
        IQueryable<Product> Products { get; }
    }
}