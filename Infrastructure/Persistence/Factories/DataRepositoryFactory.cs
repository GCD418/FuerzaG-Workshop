using FuerzaG.Domain.Ports;

namespace FuerzaG.Infrastructure.Persistence.Factories;

public abstract class DataRepositoryFactory
{
    public abstract IRepository<T> GetRepository<T>();
}