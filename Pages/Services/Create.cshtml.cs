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

    
    public List<string> ValidationErrors { get; } = new();

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        ValidationErrors.Clear();

        var result = _validator.Validate(service);
        if (!result.IsSuccess)
        {
            ValidationErrors.AddRange(result.Errors);
            return Page();
        }

        var newId = _serviceService.Create(service);
        if (newId <= 0)
        {
            ValidationErrors.Add("No se pudo crear el registro.");
            return Page();
        }

        return RedirectToPage("/Services/ServicePage");
    }
}
