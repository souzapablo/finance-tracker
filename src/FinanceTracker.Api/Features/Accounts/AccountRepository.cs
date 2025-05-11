
using Dapper;
using FinanceTracker.Api.Infra.Contracts;

namespace FinanceTracker.Api.Features.Accounts;

public class AccountRepository(
    ISqlConnectionFactory connectionFactory) : IAccountRepository
{
    public async Task<long> InsertAsync(Account account, CancellationToken cancellationToken)
    {
        using var connection = connectionFactory.CreateConnection();

        var sql = @"INSERT INTO accounts (name, user_id, balance) 
                    VALUES (@name, @userId, @balance)
                    RETURNING Id;"
        ;

        var command = new CommandDefinition(sql, account, cancellationToken: cancellationToken);
        return await connection.ExecuteScalarAsync<long>(command);
    }   
}
