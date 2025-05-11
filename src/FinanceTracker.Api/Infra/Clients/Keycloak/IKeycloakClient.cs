using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Features.Users;

namespace FinanceTracker.Api.Infra.Clients.Keycloak;

public interface IKeycloakClient
{
    Task<Result<string>> CreateUserAsync(Request command, Guid userId, CancellationToken cancellationToken);
}
