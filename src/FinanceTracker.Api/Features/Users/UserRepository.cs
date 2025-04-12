using Dapper;
using FinanceTracker.Api.Infra.Contracts;

namespace FinanceTracker.Api.Features.Users;

public class UserRepository(
    ISqlConnectionFactory connectionFactory) : IUserRepository
{
    public async Task<long> InsertAsync(User user, CancellationToken cancellation)
    {
        using var connection = connectionFactory.CreateConnection();

        var sql = @"INSERT INTO users (username, first_name, last_name, email) 
                    VALUES (@username, @firstName, @lastName, @email)
                    RETURNING Id;"
        ;

        var command = new CommandDefinition(sql, user, cancellationToken: cancellation);
        return await connection.ExecuteScalarAsync<long>(command);
    }

    public async Task UpdateExternalId(User user, long userId, CancellationToken cancellation)
    {
        using var connection = connectionFactory.CreateConnection();

        var sql = @"UPDATE users
                    SET external_id = @externalId, last_update = @lastUpdate
                    WHERE id = @id;"
        ;

        var command = new CommandDefinition(sql, new { externalId = user.ExternalId, lastUpdate = user.LastUpdate, id = userId }, cancellationToken: cancellation);
        await connection.ExecuteScalarAsync(command);
    }
}
