using System.Data;
using FuerzaG.Data.Interfaces;
using FuerzaG.Factories;
using FuerzaG.Models;

namespace FuerzaG.Data.Repositories;

public class OwnerRepository:IRepository<Owner>
{
    private readonly IDbConnectionFactory  _dbConnectionFactory;

    public OwnerRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    
    public List<Owner> GetAll()
    {
        var owners = new List<Owner>();
        using var connection = _dbConnectionFactory.CreateConnection();

        string query = @"
            SELECT 
            id,
            name,
            first_last_name,
            second_last_name,
            phone_number,
            email,
            document_number,
            address,
            created_at,
            updated_at,
            modified_by_user_id
        FROM owner
        WHERE is_active = true
        ORDER BY id ASC;";
        using var command = connection.CreateCommand();
        command.CommandText = query;
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            owners.Add(MapReaderToModel(reader));
        }

        return owners;
    }

    public Owner? GetById(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = @"
            SELECT 
            id,
            name,
            first_last_name,
            second_last_name,
            phone_number,
            email,
            document_number,
            address,
            created_at,
            updated_at,
            is_active,
            modified_by_user_id
            FROM owner
            WHERE id = @id AND is_active = true;";
        using var command = connection.CreateCommand();
        AddParameter(command,  "@id", id);
        connection.Open();
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return MapReaderToModel(reader);
        }
        return null;
    }

    public int Create(Owner entity)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = @"
            INSERT INTO owner (
                name,
                first_last_name,
                second_last_name,
                phone_number,
                email,
                document_number,
                address,
            ) VALUES (
                @name,
                @first_last_name,
                @second_last_name,
                @phone_number,
                @email,
                @document_number,
                @address,
            )
            RETURNING id;";
        using var command = connection.CreateCommand();
        AddParameter(command, "@name", entity.Name);
        AddParameter(command, "@first_last_name", entity.FirstLastname);
        AddParameter(command, "@second_last_name", entity.SecondLastname);
        AddParameter(command, "@phone_number", entity.PhoneNumber);
        AddParameter(command, "@email", entity.Email);
        AddParameter(command, "@document_number", entity.Ci);
        AddParameter(command, "@address", entity.Address);
        connection.Open();
        var result = command.ExecuteNonQuery();
        return Convert.ToInt32(result);
    }
    public bool Update(T entity);
    public bool DeleteById(int id);

    public Owner MapReaderToModel(IDataReader reader)
    {
        return new Owner
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            FirstLastname = reader.GetString(2),
            SecondLastname = reader.GetString(3),
            PhoneNumber = reader.GetString(4),
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