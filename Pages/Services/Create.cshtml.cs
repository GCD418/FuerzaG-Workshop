using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Services;
public class CreateModel : PageModel
{
    private readonly ServiceService _serviceService;

    public CreateModel(ServiceService serviceService)
    {
        _serviceService = serviceService;
    }
    [BindProperty] public Service service { get; set; } = new();

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        var newId = _serviceService.Create(service);
        if (newId <= 0)
        {
            ModelState.AddModelError(string.Empty, "No se pudo crear el registro.");
            return Page();
        }

        return RedirectToPage("/Servicess/ServicePage");
    }
}
