using Catalog.Domain.Data;
using Catalog.Domain.Services.Implementations;
using Catalog.Domain.Services.Intefaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Domain
{
    public static class ExtensionMethods
    {
        public static void AddDomainServices(this IServiceCollection service, Action<DbContextOptionsBuilder> action)
        {
            service.AddDbContext<DatabaseContext>(action, ServiceLifetime.Scoped);
            service.AddScoped<IDataProvider>(service => service.GetRequiredService<DatabaseContext>());

            service.AddScoped<ICustomerService, CustomerService>();
            service.AddScoped<IProductService, ProductService>();
        }
    }
}