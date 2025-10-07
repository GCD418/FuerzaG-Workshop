using System.Data;

namespace FuerzaG.Data.Interfaces;

public interface IRepository<T>
{
    public List<T> GetAll();
    public T GetById(int id);
    public int Create(T entity);
    public bool Update(T entity);
    public bool DeleteById(int id);
    public T MapReaderToModel(IDataReader reader);
}