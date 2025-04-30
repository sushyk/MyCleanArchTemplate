//using MyCleanArchTemplate.Application.Abstractions.Messaging;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCleanArchTemplate.Application.UseCases.Followers.StartFollowing;

public sealed record StartFollowingCommand(Guid userId, Guid FollowerId) : ICommand;

internal sealed class StartFollowingCommandHandler(IUserRepository userRepository): IRequestHandler<StartFollowingCommand>
{

    public async Task Handle(StartFollowingCommand command, CancellationToken cancellationToken)
    {
        User user = await userRepository.GetByIdAsync(command.userId, cancellationToken);

        if (user is null)
        {
            return Result(new Exception("The user is not found"));
        }
    }

    ValueTask<Unit> Handle(StartFollowingCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    ValueTask<Unit> IRequestHandler<StartFollowingCommand, Unit>.Handle(StartFollowingCommand request, CancellationToken cancellationToken)
    {
        return Unit.Value;
    }
}

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
}

public record User(Guid userId);
