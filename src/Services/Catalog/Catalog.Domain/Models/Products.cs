namespace Catalog.Domain.Models
{
    public sealed class Product
    {
        private Product() { }

        public static Product Create(Customer customer, string name, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name) || price <= 0)
            {
                throw new InvalidDataException("Invalid data");
            }

            return new() { Name = name, Price = price, CustomerId = customer.Id };
        }

        public void Update(string name, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name) || price <= 0)
            {
                throw new InvalidDataException("Invalid data");
            }

            Name = name;
            Price = price;
        }


        public Guid Id { get; private init; } = Guid.NewGuid();
        public string Name { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public Guid CustomerId { get; private set; }
    }
}