using System.Data;
using FuerzaG.Domain.Ports;
using FuerzaG.Factories;
using FuerzaG.Infrastructure.Connection;
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

        string query = "SELECT * FROM fn_get_active_services()";

        using var command = connection.CreateCommand();
        command.CommandText = query;
        connection.Open();

        using var reader = command.ExecuteReader();
        while (reader.Read())
            services.Add(MapReaderToModel(reader));

        return services;
    }

    public Service? GetById(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = "SELECT * FROM fn_get_service_by_id(@id)";

        using var command = connection.CreateCommand();
        command.CommandText = query;
        AddParameter(command, "@id", id);

        connection.Open();
        using var reader = command.ExecuteReader();
        return reader.Read() ? MapReaderToModel(reader) : null;
    }

    public int Create(Service entity)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = "SELECT fn_insert_service(@name, @type, @price, @description)";

        using var command = connection.CreateCommand();
        command.CommandText = query;

        AddParameter(command, "@name", entity.Name);
        AddParameter(command, "@type", entity.Type);
        AddParameter(command, "@price", entity.Price);
        AddParameter(command, "@description", entity.Description);

        connection.Open();
        var idObj = command.ExecuteScalar();
        return Convert.ToInt32(idObj);
    }

    public bool Update(Service entity)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = "SELECT fn_update_service(@id, @name, @type, @price, @description, @modified_by_user_id)";

        using var command = connection.CreateCommand();
        command.CommandText = query;

        AddParameter(command, "@id", entity.Id);
        AddParameter(command, "@name", entity.Name);
        AddParameter(command, "@type", entity.Type);
        AddParameter(command, "@price", entity.Price);
        AddParameter(command, "@description", entity.Description);
        AddParameter(command, "@modified_by_user_id", 9999); //TODO implement real ids

        connection.Open();
        return Convert.ToBoolean(command.ExecuteScalar());
    }

    public bool DeleteById(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = "SELECT fn_soft_delete_service(@id, @modified_by_user_id)";
        using var command = connection.CreateCommand();
        command.CommandText = query;
        AddParameter(command, "@id", id);
        AddParameter(command, "@modified_by_user_id", 8888); //TODO implement real ids
        connection.Open();
        return Convert.ToBoolean(command.ExecuteScalar());
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
            IsActive = reader.GetBoolean(7),
            ModifiedByUserId = reader.IsDBNull(8) ? null : reader.GetInt32(8)
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