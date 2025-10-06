using FuerzaG.Models;
using FuerzaG.Repository;

namespace FuerzaG.Services
{
    public class TechnicianService
    {
        private readonly TechnicianRepository _repository;

        public TechnicianService(TechnicianRepository repository)
        {
            _repository = repository;
        }

        public List<Technician> GetAllTechnicians()
        {
            return _repository.GetAll();
        }

        public Technician? GetTechnicianById(short id)
        {
            return _repository.GetById(id);
        }

        public void AddTechnician(Technician tech)
        {
            _repository.Add(tech);
        }

        public void UpdateTechnician(Technician tech)
        {
            _repository.Update(tech);
        }

        public void DeleteTechnician(short id)
        {
            _repository.Delete(id);
        }
    }
}
