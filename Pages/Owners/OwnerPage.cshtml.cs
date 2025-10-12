using FuerzaG.Domain.Entities;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Owners;

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