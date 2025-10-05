using Npgsql;

namespace FuerzaG.Data.Services;

public class DatabaseConnectionManager
{
    private static DatabaseConnectionManager _instance;
    private static readonly object _locker = new object();
    private readonly string _connectionString;
    private readonly IConfiguration _configuration;

    private DatabaseConnectionManager(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("NeonPostgres")!;
    }

    public static DatabaseConnectionManager GetInstance(IConfiguration configuration)
    {
        if (_instance == null)
        {
            lock (_locker)
            {
                if (_instance == null)
                {
                    _instance = new DatabaseConnectionManager(configuration);
                }
            }
        }
        return _instance;
    }

    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}