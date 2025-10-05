using Npgsql;

namespace FuerzaG.Data.Services;

public class DatabaseConnection
{
    private static DatabaseConnection _instance;
    private static readonly object _locker = new object();
    private readonly string _connectionString;
    private readonly IConfiguration _configuration;

    private DatabaseConnection(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("NeonPostgres")!;
    }

    public static DatabaseConnection GetInstance(IConfiguration configuration)
    {
        if (_instance == null)
        {
            lock (_locker)
            {
                if (_instance == null)
                {
                    _instance = new DatabaseConnection(configuration);
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