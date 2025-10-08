using System;
using System.Collections.Generic;
using System.Data;
using FuerzaG.Data.Interfaces;
using FuerzaG.Factories;
using FuerzaG.Models;

namespace FuerzaG.Data.Repositories
{
    public class TechnicianRepository : IRepository<Technician>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public TechnicianRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public List<Technician> GetAll()
        {
            const string sql = @"
                SELECT id, name, first_last_name, second_last_name, phone_number, email,
                       document_number, address, base_salary, created_at, updated_at,
                       is_active, modified_by_user_id
                FROM technician
                WHERE is_active = TRUE
                ORDER BY id DESC;";

            using var conn = _connectionFactory.CreateConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            using var reader = cmd.ExecuteReader();
            var list = new List<Technician>();
            while (reader.Read())
                list.Add(MapReaderToModel(reader));
            return list;
        }

        public Technician GetById(int id)
        {
            const string sql = @"
                SELECT id, name, first_last_name, second_last_name, phone_number, email,
                       document_number, address, base_salary, created_at, updated_at,
                       is_active, modified_by_user_id
                FROM technician
                WHERE id = @id;";

            using var conn = _connectionFactory.CreateConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            AddParam(cmd, "@id", id);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapReaderToModel(reader) : null;
        }

        public int Create(Technician t)
        {
            const string sql = @"
                INSERT INTO technician
                (name, first_last_name, second_last_name, phone_number, email,
                 document_number, address, base_salary, created_at, updated_at,
                 is_active, modified_by_user_id)
                VALUES
                (@name, @first_last_name, @second_last_name, @phone_number, @email,
                 @document_number, @address, @base_salary, NOW(), NOW(),
                 COALESCE(@is_active, TRUE), @modified_by_user_id)
                RETURNING id;";

            using var conn = _connectionFactory.CreateConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            AddParam(cmd, "@name", t.Name);
            AddParam(cmd, "@first_last_name", t.First_Last_Name);
            AddParam(cmd, "@second_last_name", t.Second_Last_Name);
            AddParam(cmd, "@phone_number", t.Phone_Number);
            AddParam(cmd, "@email", t.Email);
            AddParam(cmd, "@document_number", t.Document_Number);
            AddParam(cmd, "@address", t.Address);
            AddParam(cmd, "@base_salary", t.Base_Salary);
            AddParam(cmd, "@is_active", t.Is_Active);
            AddParam(cmd, "@modified_by_user_id", t.Modified_By_User_Id);

            var idObj = cmd.ExecuteScalar();
            return idObj is int i ? i : Convert.ToInt32(idObj);
        }

        public bool Update(Technician t)
        {
            const string sql = @"
                UPDATE technician SET
                    name = @name,
                    first_last_name = @first_last_name,
                    second_last_name = @second_last_name,
                    phone_number = @phone_number,
                    email = @email,
                    document_number = @document_number,
                    address = @address,
                    base_salary = @base_salary,
                    updated_at = NOW(),
                    is_active = @is_active,
                    modified_by_user_id = @modified_by_user_id
                WHERE id = @id;";

            using var conn = _connectionFactory.CreateConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            AddParam(cmd, "@id", t.Id);
            AddParam(cmd, "@name", t.Name);
            AddParam(cmd, "@first_last_name", t.First_Last_Name);
            AddParam(cmd, "@second_last_name", t.Second_Last_Name);
            AddParam(cmd, "@phone_number", t.Phone_Number);
            AddParam(cmd, "@email", t.Email);
            AddParam(cmd, "@document_number", t.Document_Number);
            AddParam(cmd, "@address", t.Address);
            AddParam(cmd, "@base_salary", t.Base_Salary);
            AddParam(cmd, "@is_active", t.Is_Active);
            AddParam(cmd, "@modified_by_user_id", t.Modified_By_User_Id);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteById(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = "DELETE FROM technician WHERE id = @id;";
            using var command = connection.CreateCommand();
            command.CommandText = sql;
            AddParam(command, "@id", id);

            connection.Open();
            var rows = command.ExecuteNonQuery();
            return rows > 0;
        }

        public Technician MapReaderToModel(IDataReader reader)
        {
            var r = (IDataRecord)reader;
            return new Technician
            {
                Id = r.GetInt32(r.GetOrdinal("id")),
                Name = r["name"] as string,
                First_Last_Name = r["first_last_name"] as string,
                Second_Last_Name = r["second_last_name"] as string,
                Phone_Number = r["phone_number"] as string,
                Email = r["email"] as string,
                Document_Number = r["document_number"] as string,
                Address = r["address"] as string,
                Base_Salary = r["base_salary"] is DBNull ? null : (decimal?)r.GetDecimal(r.GetOrdinal("base_salary")),
                Created_At = r["created_at"] is DBNull ? null : (DateTime?)r.GetDateTime(r.GetOrdinal("created_at")),
                Updated_At = r["updated_at"] is DBNull ? null : (DateTime?)r.GetDateTime(r.GetOrdinal("updated_at")),
                Is_Active = r["is_active"] is DBNull ? true : (bool)r["is_active"],
                Modified_By_User_Id = r["modified_by_user_id"] is DBNull ? null : (int?)Convert.ToInt32(r["modified_by_user_id"])
            };
        }

        private static void AddParam(IDbCommand cmd, string name, object? value)
        {
            var p = cmd.CreateParameter();
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            cmd.Parameters.Add(p);
        }
    }
}
