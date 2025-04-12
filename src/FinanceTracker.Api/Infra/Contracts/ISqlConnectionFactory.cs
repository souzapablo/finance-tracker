using Npgsql;

namespace FinanceTracker.Api.Infra.Contracts;

public interface ISqlConnectionFactory
{
    NpgsqlConnection CreateConnection();
}