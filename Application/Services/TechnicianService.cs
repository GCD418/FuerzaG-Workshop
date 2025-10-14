using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Ports; // IRepository<T>

namespace FuerzaG.Application.Services;

public class TechnicianService
{
    private readonly IRepository<Technician> _repo;

    public TechnicianService(IRepository<Technician> repo)
    {
        _repo = repo;
    }

    public List<Technician> GetAll()          => _repo.GetAll();
    public Technician? GetById(int id)        => _repo.GetById(id);
    public int Create(Technician t)           => _repo.Create(t);
    public bool Update(Technician t)          => _repo.Update(t);
    public bool DeleteById(int id)            => _repo.DeleteById(id);
}
