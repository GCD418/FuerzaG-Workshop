using System.Data;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Ports;
using FuerzaG.Infrastructure.Connection;

namespace FuerzaG.Infrastructure.Persistence;

public class OwnerRepository : IRepository<Owner>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public OwnerRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public List<Owner> GetAll()
    {
        var owners = new List<Owner>();
        using var connection = _dbConnectionFactory.CreateConnection();

        string query = "SELECT * FROM fn_get_active_owners()";

        using var command = connection.CreateCommand();
        command.CommandText = query;         
        connection.Open();

        using var reader = command.ExecuteReader();
        while (reader.Read())
            owners.Add(MapReaderToModel(reader));

        return owners;
    }

    public Owner? GetById(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = "SELECT * FROM fn_get_owner_by_id(@id)";

        using var command = connection.CreateCommand();
        command.CommandText = query;   
        AddParameter(command, "@id", id);

        connection.Open();
        using var reader = command.ExecuteReader();
        return reader.Read() ? MapReaderToModel(reader) : null;
    }

    public int Create(Owner owner)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = "SELECT fn_insert_owner(@name, @first_last_name, @second_last_name, @phone_number, @email, @document_number, @address)";

        using var command = connection.CreateCommand();
        command.CommandText = query;             

        AddParameter(command, "@name",             owner.Name);
        AddParameter(command, "@first_last_name",  owner.FirstLastname);
        AddParameter(command, "@second_last_name", owner.SecondLastname);
        AddParameter(command, "@phone_number",     owner.PhoneNumber);
        AddParameter(command, "@email",            owner.Email);
        AddParameter(command, "@document_number",  owner.Ci);
        AddParameter(command, "@address",          owner.Address);

        connection.Open();
        var idObj = command.ExecuteScalar();        
        return Convert.ToInt32(idObj);
    }

    public bool Update(Owner owner)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = "SELECT fn_update_owner(@id, @name, @first_last_name, @second_last_name, @phone_number, @email, @document_number, @address, @modified_by_user_id)";

        using var command = connection.CreateCommand();
        command.CommandText = query;           

        AddParameter(command, "@name",             owner.Name);
        AddParameter(command, "@first_last_name",  owner.FirstLastname);
        AddParameter(command, "@second_last_name", owner.SecondLastname);
        AddParameter(command, "@phone_number",     owner.PhoneNumber);
        AddParameter(command, "@email",            owner.Email);
        AddParameter(command, "@document_number",  owner.Ci);
        AddParameter(command, "@address",          owner.Address);
        AddParameter(command, "@modified_by_user_id", 9999);
        AddParameter(command, "@id",               owner.Id);

        connection.Open();
        return Convert.ToBoolean(command.ExecuteScalar());
    }

    public bool DeleteById(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        const string query = "SELECT fn_soft_delete_owner(@id, @modified_by_user_id)";
        using var command = connection.CreateCommand();
        command.CommandText = query;                  
        AddParameter(command, "@id", id);
        AddParameter(command, "@modified_by_user_id", 8888); //TODO implement real ids

        connection.Open();
        return Convert.ToBoolean(command.ExecuteScalar());
    }





    public Owner MapReaderToModel(IDataReader reader)
    {
        return new Owner
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            FirstLastname = reader.GetString(2),
            SecondLastname = reader.IsDBNull(3) ? null : reader.GetString(3),
            PhoneNumber = reader.GetInt32(4),
            Email = reader.GetString(5),
            Ci = reader.GetString(6),
            Address = reader.GetString(7),
            CreatedAt = reader.GetDateTime(8),
            UpdatedAt = reader.IsDBNull(9) ? null : reader.GetDateTime(9),
            IsActive = reader.GetBoolean(10)
        };
    }

    private void AddParameter(IDbCommand command, string name, object value)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value ?? DBNull.Value;
        command.Parameters.Add(parameter);
    }
}
