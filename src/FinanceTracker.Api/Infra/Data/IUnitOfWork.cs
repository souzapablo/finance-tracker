namespace FinanceTracker.Api.Infra.Data;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}
