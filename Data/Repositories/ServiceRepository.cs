using System.Data;
using FuerzaG.Data.Interfaces;
using FuerzaG.Factories;
using FuerzaG.Models;

namespace FuerzaG.Data.Repositories;

public class ServiceRepository : IRepository<Service>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public ServiceRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    
    public List<Service> GetAll()
    {
        var services = new List<Service>();
        using var connection = _dbConnectionFactory.CreateConnection();

        string query = @"
            SELECT 
            id,
            name,
            type,
            price,
            description,
            created_at,
            updated_at,
            is_active,
            modified_by_user_id
        FROM service
        WHERE is_active = true
        ORDER BY id ASC;";
        using var command = connection.CreateCommand();
        command.CommandText = query;
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            services.Add(MapReaderToModel(reader));
        }

        return services;
    }

    public Service? GetById(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = @"
            SELECT 
            id,
            name,
            type,
            price,
            description,
            created_at,
            updated_at,
            is_active,
            modified_by_user_id
            FROM service
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

    public int Create(Service entity)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = @"
            INSERT INTO service (
                name,
                type,
                price,
                description
            ) VALUES (
                @name,
                @type,
                @price,
                @description
            )
            RETURNING id;";
        using var command = connection.CreateCommand();
        AddParameter(command, "@name", entity.Name);
        AddParameter(command, "@type", entity.Type);
        AddParameter(command, "@price", entity.Price);
        AddParameter(command, "@description", entity.Description);
        connection.Open();
        var result = command.ExecuteNonQuery();
        return Convert.ToInt32(result);
    }

    public bool Update(Service entity)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = @"
            UPDATE service
            SET 
                name = @name,
                type = @type,
                price = @price,
                description = @description,
                updated_at = CURRENT_TIMESTAMP,
                modified_by_user_id = @modified_by_user_id
            WHERE id = @id;";
        using var command = connection.CreateCommand();
        AddParameter(command, "@name", entity.Name);
        AddParameter(command, "@type", entity.Type);
        AddParameter(command, "@price", entity.Price);
        AddParameter(command, "@description", entity.Description);
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
            UPDATE service
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

    public Service MapReaderToModel(IDataReader reader)
    {
        return new Service
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Type = reader.GetString(2),
            Price = reader.GetDecimal(3),
            Description = reader.GetString(4),
            CreatedAt = reader.GetDateTime(5),
            UpdatedAt = reader.IsDBNull(6) ? null : reader.GetDateTime(6),
            IsActive = reader.GetBoolean(7)
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