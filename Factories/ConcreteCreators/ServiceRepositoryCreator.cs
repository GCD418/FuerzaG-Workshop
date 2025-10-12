using FuerzaG.Data.Repositories;
using FuerzaG.Domain.Ports;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;
using FuerzaG.Models;

namespace FuerzaG.Factories.ConcreteCreators;

public class ServiceRepositoryCreator : DataRepositoryFactory
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public ServiceRepositoryCreator(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public override IRepository<T> GetRepository<T>()
    {
        return (IRepository<T>)(object)new ServiceRepository(_dbConnectionFactory);
    }
}