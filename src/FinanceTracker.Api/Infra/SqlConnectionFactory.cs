using FinanceTracker.Api.Infra.Contracts;
using Microsoft.Data.SqlClient;

namespace FinanceTracker.Api.Infra;

public class SqlConnectionFactory(IConfiguration configuration) : ISqlConnectionFactory
{
    private readonly string _connectionString = 
        configuration.GetConnectionString("Postgres") ??
        throw new InvalidOperationException("Connection string 'Postgres' not found.");

    public SqlConnection CreateConnection() =>
        new(_connectionString);
}