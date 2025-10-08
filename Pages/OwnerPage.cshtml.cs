using FuerzaG.Factories;
using FuerzaG.Factories.ConcreteCreators;
using FuerzaG.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace FuerzaG.Pages;

public class OwnerPage : PageModel
{
    public List<Owner> Owners { get; set; } = [];
    private readonly DataRepositoryFactory _dataRepositoryFactory;

    public OwnerPage(IDbConnectionFactory connectionFactory)
    {
        _dataRepositoryFactory = new OwnerRepositoryCreator(connectionFactory);
    }
    public void OnGet()
    {
        Owners = _dataRepositoryFactory.GetRepository<Owner>().GetAll();
    }
    
    
public IActionResult OnPostDelete(int id)
{
    var repo = _dataRepositoryFactory.GetRepository<Owner>();

    var ok = repo.DeleteById(id);
    var sigueActivo = repo.GetById(id) != null;

    TempData["Msg"] = ok
        ? (sigueActivo ? $"Se ejecutó DeleteById({id}) PERO el registro sigue activo en la BD." 
                       : $"Registro #{id} eliminado (is_active=false).")
        : $"No se actualizó ninguna fila para id={id}.";

    return RedirectToPage();
}

    
}