using System.Data;

namespace EgorEmployees.RestApi.Database;

public interface IDbConnectionFactory
{
    public Task<IDbConnection> CreateConnectionAsync();
}