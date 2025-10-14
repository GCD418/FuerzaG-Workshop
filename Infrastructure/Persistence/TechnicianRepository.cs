using System.Collections.Generic;
using FuerzaG.Domain.Entities;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;

namespace FuerzaG.Application.Services

{
    public class TechnicianService
    {
        private readonly DataRepositoryFactory _dataRepositoryFactory;

        public TechnicianService(IDbConnectionFactory connectionFactory)
        {
            _dataRepositoryFactory = new TechnicianRepositoryCreator(connectionFactory);
        }

        public List<Technician> GetAll()
        {
            return _dataRepositoryFactory.GetRepository<Technician>().GetAll();
        }

        public Technician? GetById(int id)
        {
            return _dataRepositoryFactory.GetRepository<Technician>().GetById(id);
        }

        public int Create(Technician technician)
        {
            return _dataRepositoryFactory.GetRepository<Technician>().Create(technician);
        }

        public bool Update(Technician technician)
        {
            return _dataRepositoryFactory.GetRepository<Technician>().Update(technician);
        }

        public bool DeleteById(int id)
        {
            return _dataRepositoryFactory.GetRepository<Technician>().DeleteById(id);
        }
    }
}
