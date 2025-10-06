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
    }
}
