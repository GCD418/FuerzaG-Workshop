using FuerzaG.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Domain.Entities;

namespace FuerzaG.Pages.Owners;

public class CreateModel : PageModel
{
    private readonly OwnerService  _ownerService;

    public CreateModel(OwnerService ownerService)
    {
        _ownerService = ownerService;
    }
    [BindProperty] public Owner owner { get; set; } = new();

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var newId = _ownerService.Create(owner);
        if (newId <= 0)
        {
            ModelState.AddModelError(string.Empty, "No se pudo crear el registro.");
            return Page();
        }
        return RedirectToPage("/Owners/OwnerPage");
    }
}
