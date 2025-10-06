using FuerzaG.Models;
using Npgsql;
using FuerzaG.Configuration;

namespace TuProyecto.Repository
{
    public class OwnerRepository
    {
        public List<Owner> GetAll()
        {
            var owners = new List<Owner>();
            var dbManager = DatabaseConnectionManager.GetInstance();

            using (var conn = dbManager.GetConnection())
            {
                conn.Open();
                string query = "SELECT id, names, first_lastname, second_lastname, phonenumber, email, ci, address FROM owners";

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
                            Address = reader.GetString(7)
                        });
                    }
                }
            }

            return owners;
        }
    }
}
