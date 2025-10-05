namespace FuerzaG.Data.Services;

public class DatabaseConnection
{
    private static DatabaseConnection _instance;
    private static readonly object _locker = new object();
    private readonly string _connectionString;

    private DatabaseConnection()
    {
        _connectionString = Environment.GetEnvironmentVariable("ConnectionString");
    }
}