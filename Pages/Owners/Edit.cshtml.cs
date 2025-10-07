using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Factories;                       // IDbConnectionFactory
using FuerzaG.Factories.ConcreteCreators;      // OwnerRepositoryCreator
using FuerzaG.Data.Interfaces;                 // IRepository<T>
using FuerzaG.Models;                          // Owner

namespace FuerzaG.Pages.Owners;

public class EditModel : PageModel
{
    private readonly OwnerRepositoryCreator _creator;

    public EditModel(IDbConnectionFactory connectionFactory)
        => _creator = new OwnerRepositoryCreator(connectionFactory);

    [BindProperty] public Owner Form { get; set; } = new();

    public IActionResult OnGet(int id)
    {
        var repo = _creator.GetRepository<Owner>();
        var entity = repo.GetById(id);
        if (entity is null) return RedirectToPage("/OwnerPage");

        Form = entity;
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var ok = _creator.GetRepository<Owner>().Update(Form);
        if (!ok)
        {
            ModelState.AddModelError(string.Empty, "No se pudo actualizar.");
            return Page();
        }
        return RedirectToPage("/OwnerPage");
    }
}
