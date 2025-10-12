using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Domain.Entities;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;

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
        return RedirectToPage("OwnerPage");
    }
}
