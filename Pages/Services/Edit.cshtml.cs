using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;

namespace FuerzaG.Pages.Services;

public class EditModel : PageModel
{
    private readonly ServiceService _serviceService;
    private readonly IValidator<Service> _validator;

    public List<string> ValidationErrors { get; set; } = [];

    public EditModel(ServiceService serviceService, IValidator<Service> validator)
    {
        _serviceService = serviceService;
        _validator = validator;
    }

    [BindProperty] public Service service { get; set; } = new();

    public IActionResult OnGet(int id)
    {
        var existing = _serviceService.GetById(id);
        if (existing is null) return RedirectToPage("/Services/ServicePage");

        service = existing;
        return Page();
    }

    public IActionResult OnPost()
    {
       

        var validationResult = _validator.Validate(service);
        if (validationResult.IsFailure)
        {
            ValidationErrors = validationResult.Errors;
            foreach (var error in validationResult.Errors)
                ModelState.AddModelError(string.Empty, error);

            return Page();
        }

        var ok = _serviceService.Update(service);
        if (!ok)
        {
            ModelState.AddModelError(string.Empty, "No se pudo actualizar el servicio.");
            return Page();
        }

        return RedirectToPage("/Services/ServicePage");
    }
}
