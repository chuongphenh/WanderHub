namespace WanderHub.Contract.Abstractions.Message;

public interface IDomainEvent
{
    Guid IdEvent { get; init; }
}
