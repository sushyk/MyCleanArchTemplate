using MyCleanArchTemplate.Core.Shared;

namespace MyCleanArchTemplate.Application.Abstractions.Messaging;

public interface IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}
