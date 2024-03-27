namespace Catalog.Domain.Models
{
    public sealed class Customer
    {
        private Customer() { }

        public static Customer Create(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("Invalid data");
            }

            return new() { Name = name, Email = email };
        }

        public void Update(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Invalid data");
            }

            Name = name;
            Email = email;
        }

        public Guid Id { get; private init; } = Guid.NewGuid();
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
    }
}
