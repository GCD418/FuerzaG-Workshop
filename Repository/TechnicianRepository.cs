using FuerzaG.Configuration;
using FuerzaG.Models;
using Npgsql;

namespace FuerzaG.Repository
{
    public class TechnicianRepository
    {
        private readonly DatabaseConnectionManager _db;

        public TechnicianRepository()
        {
            _db = DatabaseConnectionManager.GetInstance();
        }

        public List<Technician> GetAll()
        {
            var technicians = new List<Technician>();

            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT id, names, first_lastname, second_lastname, speciality, phonenumber, email, basesalary, is_active
                                 FROM technicians
                                 WHERE is_active = TRUE";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        technicians.Add(new Technician
                        {
                            Id = reader.GetInt16(0),
                            Names = reader.GetString(1),
                            FirstLastname = reader.GetString(2),
                            SecondLastname = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Speciality = reader.GetString(4),
                            PhoneNumber = reader.GetString(5),
                            Email = reader.GetString(6),
                            BaseSalary = reader.GetDecimal(8),
                            IsActive = reader.GetBoolean(9)
                        });
                    }
                }
            }

            return technicians;
        }

        public Technician? GetById(short id)
        {
            Technician? tech = null;

            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT id, names, first_lastname, second_lastname, speciality, phonenumber, email, basesalary, is_active
                                 FROM technicians
                                 WHERE id = @id AND is_active = TRUE";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tech = new Technician
                            {
                                Id = reader.GetInt16(0),
                                Names = reader.GetString(1),
                                FirstLastname = reader.GetString(2),
                                SecondLastname = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Speciality = reader.GetString(4),
                                PhoneNumber = reader.GetString(5),
                                Email = reader.GetString(6),
                                BaseSalary = reader.GetDecimal(8),
                                IsActive = reader.GetBoolean(9)
                            };
                        }
                    }
                }
            }

            return tech;
        }

        public void Add(Technician tech)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO technicians (names, first_lastname, second_lastname, speciality, phonenumber, email, basesalary, is_active)
                                 VALUES (@names, @first_lastname, @second_lastname, @speciality, @phonenumber, @email, @basesalary, @is_active)";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("names", tech.Names);
                    cmd.Parameters.AddWithValue("first_lastname", tech.FirstLastname);
                    cmd.Parameters.AddWithValue("second_lastname", (object?)tech.SecondLastname ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("speciality", tech.Speciality);
                    cmd.Parameters.AddWithValue("phonenumber", tech.PhoneNumber);
                    cmd.Parameters.AddWithValue("email", tech.Email);
                    cmd.Parameters.AddWithValue("basesalary", tech.BaseSalary);
                    cmd.Parameters.AddWithValue("is_active", tech.IsActive);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Technician tech)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE technicians
                                 SET names=@names, first_lastname=@first_lastname, second_lastname=@second_lastname,
                                     speciality=@speciality, phonenumber=@phonenumber, email=@email, basesalary=@basesalary
                                 WHERE id=@id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("names", tech.Names);
                    cmd.Parameters.AddWithValue("first_lastname", tech.FirstLastname);
                    cmd.Parameters.AddWithValue("second_lastname", (object?)tech.SecondLastname ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("speciality", tech.Speciality);
                    cmd.Parameters.AddWithValue("phonenumber", tech.PhoneNumber);
                    cmd.Parameters.AddWithValue("email", tech.Email);
                    cmd.Parameters.AddWithValue("basesalary", tech.BaseSalary);
                    cmd.Parameters.AddWithValue("id", tech.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(short id)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = "UPDATE technicians SET is_active = FALSE WHERE id = @id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
