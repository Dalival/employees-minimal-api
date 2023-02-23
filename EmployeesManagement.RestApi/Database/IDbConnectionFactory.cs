using System.Data;

namespace EmployeesManagement.RestApi.Database;

public interface IDbConnectionFactory
{
    public Task<IDbConnection> CreateConnectionAsync();
}