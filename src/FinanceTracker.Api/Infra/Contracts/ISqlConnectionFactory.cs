using Microsoft.Data.SqlClient;

namespace FinanceTracker.Api.Infra.Contracts
{
    public interface ISqlConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}