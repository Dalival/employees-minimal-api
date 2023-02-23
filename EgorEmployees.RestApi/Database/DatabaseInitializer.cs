using Dapper;

namespace EgorEmployees.RestApi.Database;

public class DatabaseInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DatabaseInitializer(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync("""
        CREATE TABLE IF NOT EXISTS employee
        (
            id            INT         NOT NULL AUTO_INCREMENT,
            first_name    VARCHAR(40) NOT NULL,
            second_name   VARCHAR(40) NOT NULL,
            patronymic    VARCHAR(40),
            date_of_birth DATE,
            PRIMARY KEY (id)
        );

        CREATE TABLE IF NOT EXISTS position
        (
            id    INT NOT NULL AUTO_INCREMENT,
            title VARCHAR(256),
            level INT,
            PRIMARY KEY (id)
        );

        CREATE TABLE IF NOT EXISTS employee_position
        (
            employee_id INT NOT NULL,
            position_id INT NOT NULL,
            PRIMARY KEY (employee_id, position_id),
            FOREIGN KEY (employee_id) REFERENCES employee (id) ON DELETE CASCADE,
            FOREIGN KEY (position_id) REFERENCES position (id) ON DELETE RESTRICT
        );
        """);
    }
}
