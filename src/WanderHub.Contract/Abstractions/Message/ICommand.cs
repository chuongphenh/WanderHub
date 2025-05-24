using MediatR;
using WanderHub.Contract.Shared;

namespace WanderHub.Contract.Abstractions.Message;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
