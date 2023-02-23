using Dapper;

using EgorEmployees.RestApi.Contracts.Data;
using EgorEmployees.RestApi.Database;
using EgorEmployees.RestApi.Repositories.Interfaces;

namespace EgorEmployees.RestApi.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public PositionRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<int> CreateAsync(PositionDto position)
    {
        var query = @"
            INSERT INTO position (title, level)
            VALUES (@Title, @Level);
            SELECT LAST_INSERT_ID();";

        using var connection = await _connectionFactory.CreateConnectionAsync();
        var createdPositionId = await connection.QuerySingleAsync<int>(query, position);

        return createdPositionId;
    }

    public async Task<IEnumerable<PositionDto>> GetAllAsync()
    {
        var query = "SELECT * FROM position";

        using var connection = await _connectionFactory.CreateConnectionAsync();
        var positions = await connection.QueryAsync<PositionDto>(query);

        return positions;
    }

    public async Task<PositionDto?> GetAsync(int id)
    {
        var query = "SELECT * FROM position WHERE id = @Id";

        using var connection = await _connectionFactory.CreateConnectionAsync();
        var position = await connection.QuerySingleOrDefaultAsync<PositionDto>(query, new { Id = id });

        return position;
    }

    public async Task<bool> UpdateAsync(PositionDto position)
    {
        var query = @"
            UPDATE position SET
                title = @Title,
                level = @Level
            WHERE id = @Id";

        using var connection = await _connectionFactory.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(query, position);

        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var query = "DELETE FROM position WHERE id = @Id";

        using var connection = await _connectionFactory.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(query, new { Id = id });

        return affectedRows > 0;
    }

    public async Task<bool> IsPositionInUse(int positionId)
    {
        var query = "SELECT COUNT(*) FROM employee_position WHERE position_id = @PositionId";

        using var connection = await _connectionFactory.CreateConnectionAsync();
        var count = await connection.QuerySingleAsync<int>(query, new { PositionId = positionId });

        return count > 0;
    }
}
