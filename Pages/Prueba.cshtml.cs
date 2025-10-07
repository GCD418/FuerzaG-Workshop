using System.Data;
using System.Data.Common;
using FuerzaG.Configuration;
using FuerzaG.Factories;
using FuerzaG.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace FuerzaG.Pages;

public class Prueba : PageModel
{
    public List<Brand> Brands { get; set; } = [];
    private readonly IDbConnectionFactory _connectionFactory;

    public Prueba(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public void OnGet()
    {
        Select();
    }

    void Select()
    {
        string query = @"SELECT * FROM brand";
        using (var connection =  _connectionFactory.CreateConnection())
        {
            using var command =  connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text; //Podría ser también un procedimiento almacenado
            connection.Open();
            // IDbDataAdapter adapter = new IDbDataAdapter(command);
            
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Brands.Add(new Brand
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    CreatedAt = reader.GetDateTime(2),
                    UpdatedAt = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                    IsActive = reader.GetBoolean(4)
                });
            }
            
            // NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
        }
    }
}