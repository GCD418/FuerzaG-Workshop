using FuerzaG.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;

namespace FuerzaG.Pages.Services;

public class CreateModel : PageModel
{
    private readonly ServiceService _serviceService;
    private readonly IValidator<Service> _validator;

    public List<string> ValidationErrors { get; set; } = [];

    public CreateModel(ServiceService serviceService, IValidator<Service> validator)
    {
        _serviceService = serviceService;
        _validator = validator;
    }

    [BindProperty] public Service service { get; set; } = new();

    public void OnGet() { }

    public IActionResult OnPost()
    {
        // if (!ModelState.IsValid) return Page();

        var validationResult = _validator.Validate(service);
        if (validationResult.IsFailure)
        {
            ValidationErrors = validationResult.Errors;
            foreach (var error in validationResult.Errors)
                ModelState.AddModelError(string.Empty, error);

            return Page();
        }

        var newId = _serviceService.Create(service);
        if (newId <= 0)
        {
            ModelState.AddModelError(string.Empty, "No se pudo crear el registro.");
            return Page();
        }

        return RedirectToPage("/Services/ServicePage");
    }
}
