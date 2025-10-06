using FuerzaG.Configuration;
using FuerzaG.Models;
using Npgsql;

namespace FuerzaG.Repository
{
    public class ServiceRepository
    {
        private readonly DatabaseConnectionManager _db;

        public ServiceRepository()
        {
            _db = DatabaseConnectionManager.GetInstance();
        }

        public List<Service> GetAll()
        {
            var services = new List<Service>();

            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT id, name, type, price, description, is_active
                                 FROM services
                                 WHERE is_active = TRUE";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        services.Add(new Service
                        {
                            Id = reader.GetInt16(0),
                            Name = reader.GetString(1),
                            Type = reader.GetString(2),
                            Price = reader.GetDecimal(3),
                            Description = reader.GetString(4),
                            IsActive = reader.GetBoolean(5)
                        });
                    }
                }
            }

            return services;
        }

        public Service? GetById(short id)
        {
            Service? service = null;

            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT id, name, type, price, description, is_active
                                 FROM services
                                 WHERE id = @id AND is_active = TRUE";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            service = new Service
                            {
                                Id = reader.GetInt16(0),
                                Name = reader.GetString(1),
                                Type = reader.GetString(2),
                                Price = reader.GetDecimal(3),
                                Description = reader.GetString(4),
                                IsActive = reader.GetBoolean(5)
                            };
                        }
                    }
                }
            }

            return service;
        }

        public void Add(Service service)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO services (name, type, price, description, is_active)
                                 VALUES (@name, @type, @price, @description, @is_active)";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("name", service.Name);
                    cmd.Parameters.AddWithValue("type", service.Type);
                    cmd.Parameters.AddWithValue("price", service.Price);
                    cmd.Parameters.AddWithValue("description", service.Description);
                    cmd.Parameters.AddWithValue("is_active", service.IsActive);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Service service)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE services
                                 SET name=@name, type=@type, price=@price, description=@description
                                 WHERE id=@id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("name", service.Name);
                    cmd.Parameters.AddWithValue("type", service.Type);
                    cmd.Parameters.AddWithValue("price", service.Price);
                    cmd.Parameters.AddWithValue("description", service.Description);
                    cmd.Parameters.AddWithValue("id", service.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(short id)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = "UPDATE services SET is_active = FALSE WHERE id = @id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
