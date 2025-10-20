using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace FuerzaG.Pages.Services;

public class CreateModel : PageModel
{model
    private readonly ServiceService _serviceService;
    private readonly IValidator<Service> _validator;

    public List<string> ValidationErrors { get; set; } = [];

    public CreateModel(ServiceService serviceService, IValidator<Service> validator)
    {
        _serviceService = serviceService;
        _validator = validator;
    }

    [BindProperty]
    public Service Service { get; set; } = new();

    public void OnGet() { }

    public IActionResult OnPost()
    {
        /* if (!ModelState.IsValid)
        {
            ValidationErrors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage)
                    ? "Entrada inválida."
                    : e.ErrorMessage)
                .ToList();
            return Page();
        } */

        var rawPrice = Request.Form["Service.Price"].ToString().Trim();
        rawPrice = new string(rawPrice.Where(c => char.IsDigit(c) || c == ',' || c == '.').ToArray());
        if (!decimal.TryParse(rawPrice, NumberStyles.Number, CultureInfo.GetCultureInfo("es-BO"), out var parsed))
        {
            ValidationErrors = new() { $"El valor '{rawPrice}' no es válido para Precio." };
            return Page();
        }
        Service.Price = parsed;

        var validationResult = _validator.Validate(Service);
        if (validationResult.IsFailure)
        {
            ValidationErrors = validationResult.Errors;
            return Page();
        }

        var newId = _serviceService.Create(Service);
        if (newId <= 0)
        {
            ValidationErrors = new() { "No se pudo crear el registro." };
            return Page();
        }

        return RedirectToPage("/Services/ServicePage");
    }
}
