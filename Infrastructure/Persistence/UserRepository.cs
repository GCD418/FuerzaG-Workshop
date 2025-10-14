using System.Data;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Ports;
using FuerzaG.Infrastructure.Connection;

namespace FuerzaG.Infrastructure.Persistence;

//DE INFRASTRUCTURE/PERSISTENCE
public class UserRepository : IRepository<User>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public UserRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public List<User> GetAll()
    {
        var users = new List<User>();
        using var connection = _dbConnectionFactory.CreateConnection();

        string query = "SELECT * FROM fn_get_active_users()";

        using var command = connection.CreateCommand();
        command.CommandText = query;
        connection.Open();

        using var reader = command.ExecuteReader();
        while (reader.Read())
            users.Add(MapReaderToModel(reader));

        return users;
    }

    public User? GetById(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = "SELECT * FROM fn_get_user_by_id(@id)";

        using var command = connection.CreateCommand();
        command.CommandText = query;
        AddParameter(command, "@id", id);

        connection.Open();
        using var reader = command.ExecuteReader();
        return reader.Read() ? MapReaderToModel(reader) : null;
    }

    public int Create(User user)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = "SELECT fn_insert_user(@name, @first_last_name, @second_last_name, @ci, @user_name, @password, @role)";

        using var command = connection.CreateCommand();
        command.CommandText = query;

        AddParameter(command, "@name", user.Name);
        AddParameter(command, "@first_last_name", user.FirstLastName);
        AddParameter(command, "@second_last_name", user.SecondLastName);
        AddParameter(command, "@ci", user.Ci);
        AddParameter(command, "@user_name", user.UserName);
        AddParameter(command, "@password", user.Password);
        AddParameter(command, "@role", user.Role);

        connection.Open();
        var idObj = command.ExecuteScalar();
        return Convert.ToInt32(idObj);
    }

    public bool Update(User user)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = "SELECT fn_update_user(@id, @name, @first_last_name, @second_last_name, @ci, @user_name, @password, @role, @modified_by_user_id)";

        using var command = connection.CreateCommand();
        command.CommandText = query;

        AddParameter(command, "@id", user.Id);
        AddParameter(command, "@name", user.Name);
        AddParameter(command, "@first_last_name", user.FirstLastName);
        AddParameter(command, "@second_last_name", user.SecondLastName);
        AddParameter(command, "@ci", user.Ci);
        AddParameter(command, "@user_name", user.UserName);
        AddParameter(command, "@password", user.Password);
        AddParameter(command, "@role", user.Role);
        AddParameter(command, "@modified_by_user_id", 9999); //TODO implementar ID real

        connection.Open();
        return Convert.ToBoolean(command.ExecuteScalar());
    }

    public bool DeleteById(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = "SELECT fn_soft_delete_user(@id, @modified_by_user_id)";

        using var command = connection.CreateCommand();
        command.CommandText = query;
        AddParameter(command, "@id", id);
        AddParameter(command, "@modified_by_user_id", 8888); //TODO implementar ID real

        connection.Open();
        return Convert.ToBoolean(command.ExecuteScalar());
    }

    public User MapReaderToModel(IDataReader reader)
    {
        return new User
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            FirstLastName = reader.GetString(2),
            SecondLastName = reader.IsDBNull(3) ? null : reader.GetString(3),
            Ci = reader.GetString(4),
            UserName = reader.GetString(5),
            Password = reader.GetString(6),
            Role = reader.GetString(7),
            CreatedAt = reader.GetDateTime(8),
            UpdatedAt = reader.IsDBNull(9) ? null : reader.GetDateTime(9),
            IsActive = reader.GetBoolean(10),
            ModifiedByUserId = reader.IsDBNull(11) ? null : reader.GetInt32(11)
        };
    }

    private void AddParameter(IDbCommand command, string name, object? value)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value ?? DBNull.Value;
        command.Parameters.Add(parameter);
    }
}
