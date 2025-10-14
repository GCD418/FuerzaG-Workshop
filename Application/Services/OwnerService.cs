using FuerzaG.Domain.Entities;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;

namespace FuerzaG.Application.Services;

public class OwnerService
{
    private readonly DataRepositoryFactory _dataRepositoryFactory;

    public OwnerService(IDbConnectionFactory connectionFactory)
    {
        _dataRepositoryFactory = new OwnerRepositoryCreator(connectionFactory);
    }

    public List<Owner> GetAll()
    {
        return _dataRepositoryFactory.GetRepository<Owner>().GetAll();
    }

    public Owner? GetById(int id)
    {
        return _dataRepositoryFactory.GetRepository<Owner>().GetById(id);
    }

    public int Create(Owner owner)
    {
        return _dataRepositoryFactory.GetRepository<Owner>().Create(owner);
    }

    public bool Update(Owner owner)
    {
        return _dataRepositoryFactory.GetRepository<Owner>().Update(owner);
    }

    public bool DeleteById(int id)
    {
        return _dataRepositoryFactory.GetRepository<Owner>().DeleteById(id);
    }
}