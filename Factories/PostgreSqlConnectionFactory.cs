using System.Data;
using FuerzaG.Configuration;
using Npgsql;

namespace FuerzaG.Factories;

public class PostgreSqlConnectionFactory : IDbConnectionFactory
{
    private readonly DatabaseConnectionManager _connectionManager;

    public PostgreSqlConnectionFactory(DatabaseConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionManager.ConnectionString);
    }

    public string GetProviderName()
    {
        return "PostgreSql";
    }
}