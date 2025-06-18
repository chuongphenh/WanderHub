﻿using WanderHub.Domain.Abstractions.Aggregates;
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

    public static Product CreateProduct(Guid id, string name, decimal price, string description)
    {
        //if (name.Length > 50)
            //throw new ProductFieldException(nameof(Name));

        var product = new Product(id, name, price, description);

        product.RaiseDomainEvent(new Contract.Services.V1.Product.DomainEvent.ProductCreated(Guid.NewGuid(), product.Id,
            product.Name, product.Price,
            product.Description
            ));

        return product;
    }
}
