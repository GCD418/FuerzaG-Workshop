using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Factories;
using FuerzaG.Factories.ConcreteCreators;
using FuerzaG.Data.Interfaces;
using FuerzaG.Models;

namespace FuerzaG.Pages.Services;

public class EditModel : PageModel
{
    private readonly ServiceRepositoryCreator _creator;

    public EditModel(IDbConnectionFactory connectionFactory)
        => _creator = new ServiceRepositoryCreator(connectionFactory);

    [BindProperty] public Service Form { get; set; } = new();

    public IActionResult OnGet(int id)
    {
        var repo = _creator.GetRepository<Service>();
        var entity = repo.GetById(id);
        if (entity is null) return RedirectToPage("/ServicePage");

        Form = entity;
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var ok = _creator.GetRepository<Service>().Update(Form);
        if (!ok)
        {
            ModelState.AddModelError(string.Empty, "No se pudo actualizar.");
            return Page();
        }
        return RedirectToPage("/ServicePage");
    }
}
