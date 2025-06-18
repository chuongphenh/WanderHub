using WanderHub.Contract.Abstractions.Message;

namespace WanderHub.Contract.Services.V1.Product;
public static class DomainEvent
{
    public record ProductCreated(Guid IdEvent, Guid Id, string Name, decimal Price, string Description) : IDomainEvent, ICommand;
}
