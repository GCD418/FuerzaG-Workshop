using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Services;

[Authorize(Roles = UserRoles.Manager)]
public class EditModel : PageModel
{
    private readonly ServiceService _serviceService;
    private readonly IDataProtector _protector;
    private readonly IValidator<Service> _validator;

    public List<string> ValidationErrors { get; set; } = new();

    public EditModel(ServiceService serviceService, IDataProtectionProvider provider, IValidator<Service> validator)
    {
        _serviceService = serviceService;
        _protector = provider.CreateProtector("ServiceProtector");
        _validator = validator;
    }

    [BindProperty] public string EncryptedId { get; set; } = string.Empty;
    [BindProperty] public Service Service { get; set; } = new();

    public IActionResult OnGet(string id)
    {
        var decryptedId = int.Parse(_protector.Unprotect(id));
        var service = _serviceService.GetById(decryptedId);

        if (service is null)
            return RedirectToPage("/Services/ServicePage");

        EncryptedId = id;
        Service = service;

        return Page();
    }

    public IActionResult OnPost()
    {
        ModelState.Clear();

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

        var isSuccess = _serviceService.Update(Service);

        if (!isSuccess)
        {
            ModelState.AddModelError(string.Empty, "No se pudo actualizar el servicio.");
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
