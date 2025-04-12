using Dapper;
using FinanceTracker.Api.Infra.Contracts;

namespace FinanceTracker.Api.Features.Users;

public class UserRepository(
    ISqlConnectionFactory connectionFactory) : IUserRepository
{
    public async Task<long> InsertAsync(User user, CancellationToken cancellation)
    {
        using var connection = connectionFactory.CreateConnection();

        var sql = @"INSERT INTO users (username, first_name, last_name, email, external_id, created_at, last_update) 
                    VALUES (@username, @firstName, @lastName, @email, @externalId, @createdAt, @lastUpdate)
                    RETURNING Id;"
        ;

        var command = new CommandDefinition(sql, user, cancellationToken: cancellation);
        return await connection.ExecuteScalarAsync<long>(command);
    }
}
