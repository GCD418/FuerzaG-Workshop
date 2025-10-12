using System.Data;

namespace FuerzaG.Infrastructure.Connection;

public interface IDbConnectionFactory
{
    IDbConnection  CreateConnection();

    string GetProviderName();
}