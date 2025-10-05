using System.Data;

namespace FuerzaG.Factories;

public interface IDbConnectionFactory
{
    IDbConnection  CreateConnection();

    string GetProviderName();
}