using MediatR;
using WanderHub.Contract.Shared;

namespace WanderHub.Contract.Abstractions.Message;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
