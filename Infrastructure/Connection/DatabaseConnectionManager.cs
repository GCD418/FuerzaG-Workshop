namespace FuerzaG.Infrastructure.Connection;

public class DatabaseConnectionManager
{
    private static DatabaseConnectionManager _instance;
    private static readonly object _locker = new object();
    public string ConnectionString { get; private set; }
    private readonly IConfiguration _configuration;

    private DatabaseConnectionManager(string  connectionString)
    {
        ConnectionString = connectionString;
    }

    public static DatabaseConnectionManager GetInstance(string connectionString)
    {
        if (_instance == null)
        {
            lock (_locker)
            {
                if (_instance == null)
                {
                    _instance = new DatabaseConnectionManager(connectionString);
                }
            }
        }
        return _instance;
    }

    public static DatabaseConnectionManager GetInstance()
    {
        if (_instance == null)
        {
            throw new Exception("Database connection manager not initialized");
        }
        return _instance;
    }

    public void UpdateConnectionString(string connectionString)
    {
        lock (_locker)
        {
            ConnectionString = connectionString;
        }
    }
}