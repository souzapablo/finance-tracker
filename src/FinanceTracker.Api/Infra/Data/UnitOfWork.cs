
namespace FinanceTracker.Api.Infra.Data;

public class UnitOfWork(FinanceTrackerDbContext context)
    : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken) =>
        await context.SaveChangesAsync(cancellationToken);
}
