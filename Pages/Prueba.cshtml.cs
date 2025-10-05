using System.Data;
using FuerzaG.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace FuerzaG.Pages;

public class Prueba : PageModel
{
    private readonly IConfiguration _configuration;
    public DataTable CategoriasTable { get; set; } = new DataTable();
    
    public Prueba(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void OnGet()
    {
        Select();
    }

    void Select()
    {
        var database = DatabaseConnectionManager.GetInstance(_configuration);
        string query = @"SELECT * FROM ""Brand""";
        using (NpgsqlConnection connection = database.GetConnection())
        {
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            connection.Open();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            adapter.Fill(CategoriasTable);
        }
    }
}