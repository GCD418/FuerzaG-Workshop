using FuerzaG.Models;
using Npgsql;
using FuerzaG.Configuration;

namespace TuProyecto.Repository
{
    public class OwnerRepository
    {
        private readonly DatabaseConnectionManager _db;

        public OwnerRepository()
        {
            _db = DatabaseConnectionManager.GetInstance();
        }

        public List<Owner> GetAll()
        {
            var owners = new List<Owner>();

            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT id, names, first_lastname, second_lastname, phonenumber, email, ci, address, is_active
                                 FROM owners
                                 WHERE is_active = TRUE";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        owners.Add(new Owner
                        {
                            Id = reader.GetInt16(0),
                            Names = reader.GetString(1),
                            FirstLastname = reader.GetString(2),
                            SecondLastname = reader.IsDBNull(3) ? null : reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            Email = reader.GetString(5),
                            Ci = reader.GetString(6),
                            Address = reader.GetString(7),
                            IsActive = reader.GetBoolean(8)
                        });
                    }
                }
            }

            return owners;
        }

        public Owner? GetById(short id)
        {
            Owner? owner = null;

            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT id, names, first_lastname, second_lastname, phonenumber, email, ci, address, is_active
                                 FROM owners
                                 WHERE id = @id AND is_active = TRUE";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            owner = new Owner
                            {
                                Id = reader.GetInt16(0),
                                Names = reader.GetString(1),
                                FirstLastname = reader.GetString(2),
                                SecondLastname = reader.IsDBNull(3) ? null : reader.GetString(3),
                                PhoneNumber = reader.GetString(4),
                                Email = reader.GetString(5),
                                Ci = reader.GetString(6),
                                Address = reader.GetString(7),
                                IsActive = reader.GetBoolean(8)
                            };
                        }
                    }
                }
            }

            return owner;
        }

        public void Add(Owner owner)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO owners (names, first_lastname, second_lastname, phonenumber, email, ci, address, is_active)
                                 VALUES (@names, @first_lastname, @second_lastname, @phonenumber, @email, @ci, @address, @is_active)";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("names", owner.Names);
                    cmd.Parameters.AddWithValue("first_lastname", owner.FirstLastname);
                    cmd.Parameters.AddWithValue("second_lastname", (object?)owner.SecondLastname ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("phonenumber", owner.PhoneNumber);
                    cmd.Parameters.AddWithValue("email", owner.Email);
                    cmd.Parameters.AddWithValue("ci", owner.Ci);
                    cmd.Parameters.AddWithValue("address", owner.Address);
                    cmd.Parameters.AddWithValue("is_active", owner.IsActive);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Owner owner)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE owners
                                 SET names=@names, first_lastname=@first_lastname, second_lastname=@second_lastname,
                                     phonenumber=@phonenumber, email=@email, ci=@ci, address=@address
                                 WHERE id=@id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("names", owner.Names);
                    cmd.Parameters.AddWithValue("first_lastname", owner.FirstLastname);
                    cmd.Parameters.AddWithValue("second_lastname", (object?)owner.SecondLastname ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("phonenumber", owner.PhoneNumber);
                    cmd.Parameters.AddWithValue("email", owner.Email);
                    cmd.Parameters.AddWithValue("ci", owner.Ci);
                    cmd.Parameters.AddWithValue("address", owner.Address);
                    cmd.Parameters.AddWithValue("id", owner.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(short id)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = "UPDATE owners SET is_active = FALSE WHERE id = @id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
