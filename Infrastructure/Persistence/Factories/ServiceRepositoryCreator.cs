using FuerzaG.Domain.Ports;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence;
using FuerzaG.Infrastructure.Security;

namespace FuerzaG.Infrastructure.Persistence.Factories;

public class ServiceRepositoryCreator : DataRepositoryFactory
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly ICurrentUser _currentUser; 

    public ServiceRepositoryCreator(IDbConnectionFactory dbConnectionFactory, ICurrentUser currentUser)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _currentUser = currentUser;
    }

    public override IRepository<T> GetRepository<T>()
    {
        
        return (IRepository<T>)(object)new ServiceRepository(_dbConnectionFactory, _currentUser);
    }
}
