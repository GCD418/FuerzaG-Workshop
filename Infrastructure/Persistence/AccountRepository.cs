using System.Data;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Ports;
using FuerzaG.Infrastructure.Connection;

namespace FuerzaG.Infrastructure.Persistence;

public class AccountRepository : IRepository<UserAccount>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public AccountRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public List<UserAccount> GetAll()
    {
        var accounts = new List<UserAccount>();
        using var connection = _dbConnectionFactory.CreateConnection();

        string query = "SELECT * FROM fn_get_active_accounts()";
        using var command = connection.CreateCommand();
        command.CommandText = query;
        connection.Open();

        using var reader = command.ExecuteReader();
        while (reader.Read())
            accounts.Add(MapReaderToModel(reader));

        return accounts;
    }

    public UserAccount? GetById(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = "SELECT * FROM fn_get_account_by_id(@id)";
        
        using var command = connection.CreateCommand();
        command.CommandText = query;
        AddParameter(command, "@id", id);

        connection.Open();
        using var reader = command.ExecuteReader();
        return reader.Read() ? MapReaderToModel(reader) : null;
    }
    public int Create(UserAccount userAccount)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = "SELECT fn_insert_account(@name, @first_last_name, @second_last_name, @phone_number, @email, @document_number, @user_name, @password, @role)";
        
        using var command = connection.CreateCommand();
        command.CommandText = query;

        AddParameter(command, "@name", userAccount.Name);
        AddParameter(command, "@first_last_name", userAccount.FirstLastName);
        AddParameter(command, "@second_last_name", userAccount.SecondLastName);
        AddParameter(command, "@phone_number", userAccount.PhoneNumber);
        AddParameter(command, "@email", userAccount.Email);
        AddParameter(command, "@document_number", userAccount.DocumentNumber);
        AddParameter(command, "@user_name", userAccount.UserName);
        AddParameter(command, "@password", userAccount.Password);
        AddParameter(command, "@role", userAccount.Role);

        connection.Open();
        var idObj = command.ExecuteScalar();
        return Convert.ToInt32(idObj);
    }

    public bool Update(UserAccount userAccount)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        const string query = "SELECT fn_update_account(@id, @name, @first_last_name, @second_last_name, @phone_number, @email, @document_number, @user_name, @password, @role, @modified_by_user_id)";
        using var command = connection.CreateCommand();

        command.CommandText = query;
        command.CommandType = CommandType.Text;

        AddParameter(command, "@id", userAccount.Id);
        AddParameter(command, "@name", userAccount.Name);
        AddParameter(command, "@first_last_name", userAccount.FirstLastName);
        AddParameter(command, "@second_last_name", userAccount.SecondLastName);
        AddParameter(command, "@phone_number", userAccount.PhoneNumber);
        AddParameter(command, "@email", userAccount.Email);
        AddParameter(command, "@document_number", userAccount.DocumentNumber);
        AddParameter(command, "@user_name", userAccount.UserName);
        AddParameter(command, "@password", userAccount.Password);
        AddParameter(command, "@role", userAccount.Role);
        AddParameter(command, "@modified_by_user_id", userAccount.ModifiedByUserId ?? 9999);

        connection.Open();
        return Convert.ToBoolean(command.ExecuteScalar());
    }

    public bool DeleteById(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        const string query = "SELECT fn_soft_delete_account(@id, @modified_by_user_id)";
        using var command = connection.CreateCommand();
        command.CommandText = query;
        AddParameter(command, "@id", id);
        AddParameter(command, "@modified_by_user_id", 8888); // TODO: reemplazar con ID real del usuario autenticado

        connection.Open();
        return Convert.ToBoolean(command.ExecuteScalar());
    }

    public UserAccount MapReaderToModel(IDataReader reader)
    {
        return new UserAccount
        {
            Id = reader.GetInt32(reader.GetOrdinal("id")),
            Name = reader.GetString(reader.GetOrdinal("name")),
            FirstLastName = reader.GetString(reader.GetOrdinal("first_last_name")),
            SecondLastName = reader.IsDBNull(reader.GetOrdinal("second_last_name")) ? null : reader.GetString(reader.GetOrdinal("second_last_name")),
            PhoneNumber = reader.IsDBNull(reader.GetOrdinal("phone_number")) ? 0 : reader.GetInt32(reader.GetOrdinal("phone_number")),
            Email = reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : reader.GetString(reader.GetOrdinal("email")),
            DocumentNumber = reader.IsDBNull(reader.GetOrdinal("document_number")) ? string.Empty : reader.GetString(reader.GetOrdinal("document_number")),
            UserName = reader.GetString(reader.GetOrdinal("user_name")),
            Password = reader.GetString(reader.GetOrdinal("password")),
            Role = reader.GetString(reader.GetOrdinal("role")),
            CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
            UpdatedAt = reader.IsDBNull(reader.GetOrdinal("updated_at")) ? null : reader.GetDateTime(reader.GetOrdinal("updated_at")),
            IsActive = reader.GetBoolean(reader.GetOrdinal("is_active")),
            ModifiedByUserId = reader.IsDBNull(reader.GetOrdinal("modified_by_user_id")) ? null : reader.GetInt32(reader.GetOrdinal("modified_by_user_id"))
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