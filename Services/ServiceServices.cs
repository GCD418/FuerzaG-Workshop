using FuerzaG.Models;
using FuerzaG.Repository;

namespace FuerzaG.Services
{
    public class ServiceServices
    {
        private readonly ServiceRepository _repository;

        public ServiceServices(ServiceRepository repository)
        {
            _repository = repository;
        }

        public List<Service> GetAllServices()
        {
            return _repository.GetAll();
        }

        public Service? GetServiceById(short id)
        {
            return _repository.GetById(id);
        }

        public void AddService(Service service)
        {
            _repository.Add(service);
        }

        public void UpdateService(Service service)
        {
            _repository.Update(service);
        }

        public void DeleteService(short id)
        {
            _repository.Delete(id);
        }
    }
}