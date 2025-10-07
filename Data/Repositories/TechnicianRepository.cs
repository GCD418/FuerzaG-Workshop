using System.Data;
using FuerzaG.Data.Interfaces;
using FuerzaG.Factories;
using FuerzaG.Models;

namespace FuerzaG.Data.Repositories;

public class TechnicianRepository : IRepository<Technician>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public TechnicianRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    
    public List<Technician> GetAll()
    {
        var technicians = new List<Technician>();
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
            base_salary,
            created_at,
            updated_at,
            is_active,
            modified_by_user_id
        FROM technician
        WHERE is_active = true
        ORDER BY id ASC;";
        using var command = connection.CreateCommand();
        command.CommandText = query;
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            technicians.Add(MapReaderToModel(reader));
        }

        return technicians;
    }

    public Technician? GetById(int id)
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
            base_salary,
            created_at,
            updated_at,
            is_active,
            modified_by_user_id
            FROM technician
            WHERE id = @id AND is_active = true;";
        using var command = connection.CreateCommand();
        AddParameter(command, "@id", id);
        connection.Open();
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return MapReaderToModel(reader);
        }
        return null;
    }

    public int Create(Technician entity)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = @"
            INSERT INTO technician (
                name,
                first_last_name,
                second_last_name,
                phone_number,
                email,
                document_number,
                address,
                base_salary
            ) VALUES (
                @name,
                @first_last_name,
                @second_last_name,
                @phone_number,
                @email,
                @document_number,
                @address,
                @base_salary
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
        AddParameter(command, "@base_salary", entity.BaseSalary);
        connection.Open();
        var result = command.ExecuteNonQuery();
        return Convert.ToInt32(result);
    }

    public bool Update(Technician entity)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = @"
            UPDATE technician
            SET 
                name = @name,
                first_last_name = @first_last_name,
                second_last_name = @second_last_name,
                phone_number = @phone_number,
                email = @email,
                document_number = @document_number,
                address = @address,
                base_salary = @base_salary,
                updated_at = CURRENT_TIMESTAMP,
                modified_by_user_id = @modified_by_user_id
            WHERE id = @id;";
        using var command = connection.CreateCommand();
        AddParameter(command, "@name", entity.Name);
        AddParameter(command, "@first_last_name", entity.FirstLastname);
        AddParameter(command, "@second_last_name", entity.SecondLastname);
        AddParameter(command, "@phone_number", entity.PhoneNumber);
        AddParameter(command, "@email", entity.Email);
        AddParameter(command, "@document_number", entity.Ci);
        AddParameter(command, "@address", entity.Address);
        AddParameter(command, "@base_salary", entity.BaseSalary);
        AddParameter(command, "@id", entity.Id);
        AddParameter(command, "@modified_by_user_id", 9999);
        connection.Open();
        int rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    public bool DeleteById(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = @"
            UPDATE technician
            SET 
                is_active = false,
                updated_at = CURRENT_TIMESTAMP,
                modified_by_user_id = @modified_by_user_id
            WHERE id = @id;";
        using var command = connection.CreateCommand();
        AddParameter(command, "@id", id);
        AddParameter(command, "@modified_by_user_id", 8888);
        connection.Open();
        int rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    public Technician MapReaderToModel(IDataReader reader)
    {
        return new Technician
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            FirstLastname = reader.GetString(2),
            SecondLastname = reader.IsDBNull(3) ? null : reader.GetString(3),
            PhoneNumber = reader.GetString(4),
            Email = reader.GetString(5),
            Ci = reader.GetString(6),
            Address = reader.GetString(7),
            BaseSalary = reader.GetDecimal(8),
            CreatedAt = reader.GetDateTime(9),
            UpdatedAt = reader.IsDBNull(10) ? null : reader.GetDateTime(10),
            IsActive = reader.GetBoolean(11)
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