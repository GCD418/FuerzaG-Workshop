using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;
using FuerzaG.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Services;

public class CreateModel : PageModel
{
    private readonly ServiceService _serviceService;
    private readonly IValidator<Service> _validator; 

    public CreateModel(ServiceService serviceService, IValidator<Service> validator)
    {
        _serviceService = serviceService;
        _validator = validator;
    }

    [BindProperty] public Service service { get; set; } = new();

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        
        var validation = _validator.Validate(service);
        if (!validation.IsSuccess)
        {
            foreach (var error in validation.Errors)
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
