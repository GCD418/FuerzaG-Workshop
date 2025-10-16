using System.Data;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Ports;
using FuerzaG.Infrastructure.Connection;

namespace FuerzaG.Infrastructure.Persistence;

public class LoginRepository : ILoginRepository
{
    
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public LoginRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public UserAccount? GetByUserName(string userName)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = "SELECT * FROM fn_get_account_by_username(@user_name)";

        using var command = connection.CreateCommand();
        command.CommandText = query;
        AddParameter(command, "@user_name", userName);

        connection.Open();
        using var reader = command.ExecuteReader();
        return reader.Read() ? MapReaderToModel(reader) : null;
    }

    public bool IsUserNameUsed(string userName)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = "SELECT fn_account_exists_by_username(@user_name)";
        using var command = connection.CreateCommand();
        command.CommandText = query;
        AddParameter(command, "@user_name", userName);

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
    }}