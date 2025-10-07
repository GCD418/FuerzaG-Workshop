using FuerzaG.Data.Interfaces;

namespace FuerzaG.Factories;

public abstract class DataRepositoryFactory
{
    public abstract IRepository<T> GetRepository<T>();
}