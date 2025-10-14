using FuerzaG.Data.Repositories;
using FuerzaG.Domain.Ports;
using FuerzaG.Factories.ConcreteCreators;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;
using FuerzaG.Models;

namespace FuerzaG.Infrastructure.Factories;
//DE INFRASTRUCTURE/PERSISTENCE/FACTORIES
public class UserRepositoryCreator : DataRepositoryFactory
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public UserRepositoryCreator(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public override IRepository<T> GetRepository<T>()
    {
        return (IRepository<T>)(object)new UserRepository(_dbConnectionFactory);
    }
}
