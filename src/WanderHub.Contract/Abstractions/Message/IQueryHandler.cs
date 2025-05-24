﻿using MediatR;
using WanderHub.Contract.Shared;

namespace WanderHub.Contract.Abstractions.Message;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
