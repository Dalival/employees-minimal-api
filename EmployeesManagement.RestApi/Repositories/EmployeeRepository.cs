using Dapper;

using EmployeesManagement.RestApi.Contracts.Data;
using EmployeesManagement.RestApi.Database;
using EmployeesManagement.RestApi.Repositories.Interfaces;

namespace EmployeesManagement.RestApi.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public EmployeeRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<int> CreateAsync(EmployeeDto employee)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var createEmployeeQuery = @"
            INSERT INTO employee (first_name, second_name, patronymic, date_of_birth)
            VALUES (@FirstName, @SecondName, @Patronymic, @DateOfBirth);
            SELECT LAST_INSERT_ID();";

        var createdEmployeeId = await connection.QuerySingleAsync<int>(createEmployeeQuery, employee);

        try
        {
            var assignToPositionQuery = "INSERT INTO employee_position (employee_id, position_id) VALUES (@EmployeeId, @PositionId)";
            foreach (var position in employee.Positions)
            {
                await connection.ExecuteAsync(assignToPositionQuery, new { EmployeeId = createdEmployeeId, PositionId = position.Id });
            }
        }
        catch
        {
            transaction.Rollback();
            return default;
        }

        transaction.Commit();

        return createdEmployeeId;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var employees = await GetIncludingPositionsAsync();
        return employees.Values;
    }

    public async Task<EmployeeDto?> GetAsync(int id)
    {
        var employees = await GetIncludingPositionsAsync(id);
        return employees.Values.SingleOrDefault();
    }

    public async Task<bool> UpdateAsync(EmployeeDto employee)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var updateEmployeesQuery = @"
            UPDATE employee SET
                first_name = @FirstName,
                second_name = @SecondName,
                patronymic = @Patronymic,
                date_of_birth = @DateOfBirth
            WHERE id = @Id";
        var affectedRows = await connection.ExecuteAsync(updateEmployeesQuery, employee);

        var unassignFromPositionsQuery = "DELETE FROM employee_position WHERE employee_id = @EmployeeId";
        affectedRows += await connection.ExecuteAsync(unassignFromPositionsQuery, new { EmployeeId = employee.Id });

        try
        {
            var assignToPositionsQuery = "INSERT INTO employee_position (employee_id, position_id) VALUES (@EmployeeId, @PositionId)";
            foreach (var position in employee.Positions)
            {
                affectedRows += await connection.ExecuteAsync(assignToPositionsQuery, new { EmployeeId = employee.Id, PositionId = position.Id });
            }
        }
        catch
        {
            transaction.Rollback();
            return false;
        }

        transaction.Commit();
        return affectedRows > 0;
        //TODO check if transaction rollbacks automatically when exception was thrown in the middle
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var query = "DELETE FROM employee WHERE id = @Id";

        using var connection = await _connectionFactory.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(query, new { Id = id });

        return affectedRows > 0;
    }

    private async Task<Dictionary<int, EmployeeDto>> GetIncludingPositionsAsync(int employeeId = default)
    {
        var query = @"
            SELECT e.*, p.*
            FROM employee e
                LEFT JOIN employee_position ep ON ep.employee_id = e.id
                LEFT JOIN position p ON p.id = ep.position_id";

        if (employeeId != default)
        {
            query += "\nWHERE e.id = @Id";
        }

        var employees = new Dictionary<int, EmployeeDto>();
        using var connection = await _connectionFactory.CreateConnectionAsync();
        await connection.QueryAsync<EmployeeDto, PositionDto, EmployeeDto>(
            query,
            (employee, position) =>
            {
                if (!employees.TryGetValue(employee.Id, out var e))
                {
                    e = employee;
                    employees.Add(e.Id, e);
                }

                if (position is not null)
                {
                    e.Positions!.Add(position);
                }

                return e;
            },
            employeeId == default ? null : new { Id = employeeId });

        return employees;
    }
}
