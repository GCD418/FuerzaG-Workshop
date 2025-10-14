using FuerzaG.Domain.Entities;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;

namespace FuerzaG.Application.Services;

public class ServiceService
{
    private readonly DataRepositoryFactory _dataRepositoryFactory;

    public ServiceService(IDbConnectionFactory connectionFactory)
    {
        _dataRepositoryFactory = new ServiceRepositoryCreator(connectionFactory);
    }

    
    public List<Service> GetAll()
    {
        return _dataRepositoryFactory.GetRepository<Service>().GetAll();
    }

  
    public Service? GetById(int id)
    {
        return _dataRepositoryFactory.GetRepository<Service>().GetById(id);
    }

    
    public int Create(Service service)
    {
        return _dataRepositoryFactory.GetRepository<Service>().Create(service);
    }

    
    public bool Update(Service service)
    {
        return _dataRepositoryFactory.GetRepository<Service>().Update(service);
    }

    
    public bool DeleteById(int id)
    {
        return _dataRepositoryFactory.GetRepository<Service>().DeleteById(id);
    }
}