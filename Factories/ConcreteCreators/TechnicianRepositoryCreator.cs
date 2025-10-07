using FuerzaG.Data.Interfaces;
using FuerzaG.Data.Repositories;
using FuerzaG.Factories;

namespace FuerzaG.Factories.ConcreteCreators
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
