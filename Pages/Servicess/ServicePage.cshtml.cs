using FuerzaG.Factories;
using FuerzaG.Factories.ConcreteCreators;
using FuerzaG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages;

public class ServicePage : PageModel
{
    public List<Service> Services { get; set; } = new();
    private readonly DataRepositoryFactory _dataRepositoryFactory;

    public ServicePage(IDbConnectionFactory connectionFactory)
    {
        _dataRepositoryFactory = new ServiceRepositoryCreator(connectionFactory);
    }

    public void OnGet()
    {
        Services = _dataRepositoryFactory.GetRepository<Service>().GetAll();
    }

    public IActionResult OnPostDelete(int id)
    {
        var repo = _dataRepositoryFactory.GetRepository<Service>();

        var ok = repo.DeleteById(id);
        var sigueActivo = repo.GetById(id) != null;

        TempData["Msg"] = ok
            ? (sigueActivo ? $"Se ejecutó DeleteById({id}) PERO el registro sigue activo en la BD."
                           : $"Registro #{id} eliminado (is_active=false).")
            : $"No se actualizó ninguna fila para id={id}.";

        return RedirectToPage();
    }
}
