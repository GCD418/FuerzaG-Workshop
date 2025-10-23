using FuerzaG.Domain.Ports;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Security; 


namespace FuerzaG.Infrastructure.Persistence.Factories;
public class AccountRepositoryCreator : DataRepositoryFactory
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    private readonly ICurrentUser _currentUser;
    
    public AccountRepositoryCreator(IDbConnectionFactory dbConnectionFactory, ICurrentUser currentUser)                  // <-- NUEVO parámetro
    {
        _dbConnectionFactory = dbConnectionFactory;
        _currentUser = currentUser;
    }

    
    public override IRepository<T> GetRepository<T>()
    {
        return (IRepository<T>)(object)new ServiceRepository(_dbConnectionFactory, _currentUser);
    }
}
