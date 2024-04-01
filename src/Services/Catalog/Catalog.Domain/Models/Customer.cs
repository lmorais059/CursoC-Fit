using System.Net;
using Catalog.Domain.Errors;

namespace Catalog.Domain.Models
{
    public sealed class Customer
    {
        private Customer() { }

        public static Customer Create(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                throw new AppError("Invalid data", HttpStatusCode.BadRequest);
            }

            return new() { Name = name, Email = email };
        }

        public void Update(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                throw new AppError("Invalid data", HttpStatusCode.BadRequest);
            }

            Name = name;
            Email = email;
        }

        public Guid Id { get; private init; } = Guid.NewGuid();
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
    }
}
