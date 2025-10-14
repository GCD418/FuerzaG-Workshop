using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Factories;
using FuerzaG.Factories.ConcreteCreators;
using FuerzaG.Domain.Entities;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;

using FuerzaG.Models;

namespace FuerzaG.Pages.Services;
public class CreateModel : PageModel
{
    private readonly ServiceRepositoryCreator _creator;

    public CreateModel(IDbConnectionFactory connectionFactory)
        => _creator = new ServiceRepositoryCreator(connectionFactory);

    [BindProperty]
    public Service Form { get; set; } = new();

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        var id = _creator.GetRepository<Service>().Create(Form);
        if (id <= 0)
        {
            ModelState.AddModelError(string.Empty, "No se pudo crear el registro.");
            return Page();
        }

        return RedirectToPage("/Servicess/ServicePage");
    }
}
