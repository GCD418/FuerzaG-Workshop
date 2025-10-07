using FuerzaG.Data.Interfaces;
using FuerzaG.Data.Repositories;
using FuerzaG.Models;

namespace FuerzaG.Factories.ConcreteCreators;

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