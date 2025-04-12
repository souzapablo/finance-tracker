using FinanceTracker.Api.Infra.Contracts;
using Npgsql;

namespace FinanceTracker.Api.Infra;

public class SqlConnectionFactory(IConfiguration configuration) : ISqlConnectionFactory
{
    private readonly string _connectionString = 
        configuration.GetConnectionString("Postgres") ??
        throw new InvalidOperationException("Connection string 'Postgres' not found.");

    public NpgsqlConnection CreateConnection() =>
        new(_connectionString);
}