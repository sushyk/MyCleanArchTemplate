using MyCleanArchTemplate.Domain.Abstractions.Persistence;

namespace MyCleanArchTemplate.Adapter.Persistence;

public sealed class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
