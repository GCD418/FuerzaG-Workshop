using FuerzaG.Data.Repositories;
using FuerzaG.Domain.Ports;
using FuerzaG.Factories;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;

namespace FuerzaG.Factories.ConcreteCreators;
//DE FACTORIES/CONCRETE CREATORS 
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
