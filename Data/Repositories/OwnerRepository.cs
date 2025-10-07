using System.Data;
using FuerzaG.Data.Interfaces;
using FuerzaG.Factories;
using FuerzaG.Models;

namespace FuerzaG.Data.Repositories;

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
            WHERE is_active = true
            ORDER BY id ASC;";

        using var command = connection.CreateCommand();
        command.CommandText = query;                 // ✅ asignar SQL
        connection.Open();

        using var reader = command.ExecuteReader();
        while (reader.Read())
            owners.Add(MapReaderToModel(reader));

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
        command.CommandText = query;                 // ✅ asignar SQL
        AddParameter(command, "@id", id);

        connection.Open();
        using var reader = command.ExecuteReader();
        return reader.Read() ? MapReaderToModel(reader) : null;
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
                address
            ) VALUES (
                @name,
                @first_last_name,
                @second_last_name,
                @phone_number,
                @email,
                @document_number,
                @address
            )
            RETURNING id;";

        using var command = connection.CreateCommand();
        command.CommandText = query;                 // ✅ asignar SQL

        AddParameter(command, "@name",             entity.Name);
        AddParameter(command, "@first_last_name",  entity.FirstLastname);
        AddParameter(command, "@second_last_name", entity.SecondLastname);
        AddParameter(command, "@phone_number",     entity.PhoneNumber);
        AddParameter(command, "@email",            entity.Email);
        AddParameter(command, "@document_number",  entity.Ci);
        AddParameter(command, "@address",          entity.Address);

        connection.Open();
        var idObj = command.ExecuteScalar();        // ✅ usar ExecuteScalar con RETURNING
        return Convert.ToInt32(idObj);
    }

    public bool Update(Owner entity)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        string query = @"
            UPDATE owner
            SET 
                name = @name,
                first_last_name = @first_last_name,
                second_last_name = @second_last_name,
                phone_number = @phone_number,
                email = @email,
                document_number = @document_number,
                address = @address,
                updated_at = CURRENT_TIMESTAMP,
                modified_by_user_id = @modified_by_user_id
            WHERE id = @id;";

        using var command = connection.CreateCommand();
        command.CommandText = query;                 // ✅ asignar SQL

        AddParameter(command, "@name",             entity.Name);
        AddParameter(command, "@first_last_name",  entity.FirstLastname);
        AddParameter(command, "@second_last_name", entity.SecondLastname);
        AddParameter(command, "@phone_number",     entity.PhoneNumber);
        AddParameter(command, "@email",            entity.Email);
        AddParameter(command, "@document_number",  entity.Ci);
        AddParameter(command, "@address",          entity.Address);
        AddParameter(command, "@modified_by_user_id", 9999); // TODO
        AddParameter(command, "@id",               entity.Id); // ✅ faltaba

        connection.Open();
        int rows = command.ExecuteNonQuery();
        return rows > 0;
    }

    public bool DeleteById(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        const string query = @"
            UPDATE owner
            SET 
                is_active = false,
                updated_at = CURRENT_TIMESTAMP,
                modified_by_user_id = @modified_by_user_id
            WHERE id = @id;";

        using var command = connection.CreateCommand();
        command.CommandText = query;                         // ¡IMPRESCINDIBLE!
        AddParameter(command, "@id", id);
        AddParameter(command, "@modified_by_user_id", 8888);

        connection.Open();
        var rows = command.ExecuteNonQuery();

        // --- DEBUG: lee el valor actual en BD para confirmar
        using (var check = connection.CreateCommand())
        {
            check.CommandText = "SELECT is_active, current_database() db FROM owner WHERE id=@id;";
            AddParameter(check, "@id", id);
            using var r = check.ExecuteReader();
            if (r.Read())
            {
                var isActive = (bool)r["is_active"];
                var dbName   = r["db"]?.ToString();
                Console.WriteLine($"[OwnerRepository] id={id} -> is_active={isActive} (db={dbName})");
            }
            else
            {
                Console.WriteLine($"[OwnerRepository] id={id} no existe.");
            }
        }

        return rows > 0;
    }




    public Owner MapReaderToModel(IDataReader reader)
    {
        return new Owner
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            FirstLastname = reader.GetString(2),
            SecondLastname = reader.IsDBNull(3) ? null : reader.GetString(3),
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
