using FuerzaG.Domain.Ports;   
using FuerzaG.Infrastructure.Connection;   
using FuerzaG.Data.Repositories; 

namespace FuerzaG.Infrastructure.Persistence.Factories
{
    public class TechnicianRepositoryCreator : DataRepositoryFactory
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public TechnicianRepositoryCreator(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public override IRepository<T> GetRepository<T>()
        {
            
            return (IRepository<T>)(object)new TechnicianRepository(_dbConnectionFactory);
        }
    }
}
