

namespace MyCleanArchTemplate.Application.Abstractions.Messaging;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    //Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
}
