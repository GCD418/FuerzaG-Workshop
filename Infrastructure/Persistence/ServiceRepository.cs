using System.Data;
using FuerzaG.Infrastructure.Connection; 
using FuerzaG.Domain.Ports;
using FuerzaG.Models; 

namespace FuerzaG.Data.Repositories
{
    public class ServiceRepository : IRepository<Service>
    {
        private readonly IDbConnectionFactory _db;
        public ServiceRepository(IDbConnectionFactory db) => _db = db;

        public List<Service> GetAll()
        {
            var list = new List<Service>();
            using var con = _db.CreateConnection();
            const string sql = @"
                SELECT id, name, type, price, description, created_at, updated_at
                FROM service
                ORDER BY name ASC;"; 
            using var cmd = con.CreateCommand();
            cmd.CommandText = sql;
            con.Open();
            using var r = cmd.ExecuteReader();
            while (r.Read()) list.Add(Map(r));
            return list;
        }

        public Service? GetById(int id)
        {
            using var con = _db.CreateConnection();
            const string sql = @"
                SELECT id, name, type, price, description, created_at, updated_at
                FROM service
                WHERE id=@id;";
            using var cmd = con.CreateCommand();
            cmd.CommandText = sql;
            Add(cmd, "@id", id);
            con.Open();
            using var r = cmd.ExecuteReader();
            return r.Read() ? Map(r) : null;
        }

        public int Create(Service s)
        {
            using var con = _db.CreateConnection();
            const string sql = @"
                INSERT INTO service (name, type, price, description, created_at)
                VALUES (@n, @t, @p, @d, NOW())
                RETURNING id;";
            using var cmd = con.CreateCommand();
            cmd.CommandText = sql;
            Add(cmd, "@n", s.Name);
            Add(cmd, "@t", s.Type);
            Add(cmd, "@p", s.Price);
            Add(cmd, "@d", (object?)s.Description ?? DBNull.Value);
            con.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool Update(Service s)
        {
            using var con = _db.CreateConnection();
            const string sql = @"
                UPDATE service
                   SET name=@n, type=@t, price=@p, description=@d, updated_at=NOW()
                 WHERE id=@id;";
            using var cmd = con.CreateCommand();
            cmd.CommandText = sql;
            Add(cmd, "@n", s.Name);
            Add(cmd, "@t", s.Type);
            Add(cmd, "@p", s.Price);
            Add(cmd, "@d", (object?)s.Description ?? DBNull.Value);
            Add(cmd, "@id", s.Id);
            con.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteById(int id)
        {
            using var con = _db.CreateConnection();
            const string sql = "DELETE FROM service WHERE id=@id;";
            using var cmd = con.CreateCommand();
            cmd.CommandText = sql;
            Add(cmd, "@id", id);
            con.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public Service MapReaderToModel(IDataReader r) => Map(r);

        private static Service Map(IDataRecord r) => new()
        {
            Id          = r.GetInt32(r.GetOrdinal("id")),
            Name        = r.GetString(r.GetOrdinal("name")),
            Type        = r.GetString(r.GetOrdinal("type")),
            Price       = r.GetDecimal(r.GetOrdinal("price")),
            Description = r.IsDBNull(r.GetOrdinal("description")) ? null : r.GetString(r.GetOrdinal("description")),
            CreatedAt   = r.GetDateTime(r.GetOrdinal("created_at")),
            UpdatedAt   = r.IsDBNull(r.GetOrdinal("updated_at")) ? null : r.GetDateTime(r.GetOrdinal("updated_at"))
        };

        private static void Add(IDbCommand c, string n, object v)
        {
            var p = c.CreateParameter();
            p.ParameterName = n;
            p.Value = v ?? DBNull.Value;
            c.Parameters.Add(p);
        }
    }
}
