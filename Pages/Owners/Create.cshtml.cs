using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Factories;                       // IDbConnectionFactory
using FuerzaG.Factories.ConcreteCreators;      // OwnerRepositoryCreator
using FuerzaG.Data.Interfaces;                 // IRepository<T>
using FuerzaG.Models;                          // Owner

namespace FuerzaG.Pages.Owners;

public class CreateModel : PageModel
{
    private readonly OwnerRepositoryCreator _creator;

    public CreateModel(IDbConnectionFactory connectionFactory)
        => _creator = new OwnerRepositoryCreator(connectionFactory);

    [BindProperty] public Owner Form { get; set; } = new();

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var id = _creator.GetRepository<Owner>().Create(Form);
        if (id <= 0)
        {
            ModelState.AddModelError(string.Empty, "No se pudo crear el registro.");
            return Page();
        }
        return RedirectToPage("/OwnerPage");
    }
}
