using MediatR;
using WanderHub.Contract.Shared;

namespace WanderHub.Contract.Abstractions.Message;
public interface ICommandHandler<TCommand> : IRequestHandler<ICommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}

