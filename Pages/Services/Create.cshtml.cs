using FuerzaG.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;

namespace FuerzaG.Pages.Services;

[Authorize(Roles = UserRoles.Manager)]
public class CreateModel : PageModel
{
    private readonly ServiceService _serviceService;
    private readonly IValidator<Service> _validator;

    public List<string> ValidationErrors { get; set; } = new();

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
        ModelState.Clear();

        var rawPrice = (Request.Form["Service.Price"].ToString() ?? string.Empty).Trim();
        rawPrice = new string(rawPrice.Where(c => char.IsDigit(c) || c == ',' || c == '.').ToArray());

        if (!decimal.TryParse(rawPrice, NumberStyles.Number, CultureInfo.GetCultureInfo("es-BO"), out var parsed) &&
            !decimal.TryParse(rawPrice, NumberStyles.Number, CultureInfo.GetCultureInfo("es-ES"), out parsed) &&
            !decimal.TryParse(rawPrice, NumberStyles.Number, CultureInfo.InvariantCulture, out parsed))
        {
            ValidationErrors = new() { $"El valor '{rawPrice}' no es válido para Precio." };
            return Page();
        }

        Service.Price = parsed;

        var validationResult = _validator.Validate(Service);
        if (validationResult.IsFailure)
        {
            ValidationErrors = validationResult.Errors;

            foreach (var error in validationResult.Errors)
            {
                var fieldName = MapErrorToField(error);
                if (!string.IsNullOrEmpty(fieldName))
                    ModelState.AddModelError($"Service.{fieldName}", error);
                else
                    ModelState.AddModelError(string.Empty, error);
            }
            return Page();
        }

        var newId = _serviceService.Create(Service);
        if (newId <= 0)
        {
            ModelState.AddModelError(string.Empty, "No se pudo crear el registro.");
            return Page();
        }

        return RedirectToPage("/Services/ServicePage");
    }

    private string MapErrorToField(string error)
    {
        var errorLower = error.ToLower();

        if (errorLower.Contains("nombre"))
            return "Name";

        if (errorLower.Contains("tipo"))
            return "Type";

        if (errorLower.Contains("precio"))
            return "Price";

        if (errorLower.Contains("descripción") || errorLower.Contains("descripcion"))
            return "Description";

        return string.Empty;
    }
}
