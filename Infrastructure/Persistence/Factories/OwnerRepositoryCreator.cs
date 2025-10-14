using FuerzaG.Domain.Ports;
using FuerzaG.Infrastructure.Connection;

namespace FuerzaG.Infrastructure.Persistence.Factories;

public class OwnerRepositoryCreator : DataRepositoryFactory
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public OwnerRepositoryCreator(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public override IRepository<T> GetRepository<T>()
    {
        return (IRepository<T>)(object)new OwnerRepository(_dbConnectionFactory);
    }
}