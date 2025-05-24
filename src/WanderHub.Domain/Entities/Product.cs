using WanderHub.Domain.Abstractions.Aggregates;
using WanderHub.Domain.Abstractions.Entities;

namespace WanderHub.Domain.Entities;
public class Product : AggregateRoot<Guid>, IAuditableEntity
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string Description { get; private set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }

    public Product(Guid id, string name, decimal price, string description)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }
}
