using FuerzaG.Configuration;
using FuerzaG.Models;
using Npgsql;
using TuProyecto.Repository;

namespace FuerzaG.Services
{
    public class OwnerService
    {
        private readonly OwnerRepository _repository;

        public OwnerService(OwnerRepository repository)
        {
            _repository = repository;
        }

        public List<Owner> GetAllOwners()
        {
            return _repository.GetAll();
        }

        public Owner? GetOwnerById(short id)
        {
            return _repository.GetById(id);
        }

        public void AddOwner(Owner owner)
        {
            _repository.Add(owner);
        }

        public void UpdateOwner(Owner owner)
        {
            _repository.Update(owner);
        }

        public void DeleteOwner(short id)
        {
            _repository.Delete(id);
        }
    }
}
