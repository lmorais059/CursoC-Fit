namespace Catalog.Domain.Models{

    public class Customer{
        public Guid Id { get; private init; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}