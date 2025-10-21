using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Domain.Services.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;

namespace FuerzaG.Pages.Owners;


[Authorize(Roles = UserRoles.Manager)]
public class EditModel : PageModel
{
    private readonly OwnerService  _ownerService;
    private readonly IValidator<Owner> _validator;
    private readonly IDataProtector _protector;
    
    public List<string> ValidationErrors { get; set; } = [];

    public EditModel(OwnerService ownerService, IValidator<Owner> validator, IDataProtectionProvider provider)
    {
        _ownerService = ownerService;
        _validator = validator;
        _protector = provider.CreateProtector("OwnerProtector");
    }

    
    [BindProperty] public Owner Owner { get; set; } = new();
    public IActionResult OnGet(string id)
    {
        var decryptedId = int.Parse(_protector.Unprotect(id));
        var owner = _ownerService.GetById(decryptedId);
        if (owner is null) return RedirectToPage("/Owners/OwnerPage");

        Owner = owner;
        return Page();
    }

    public IActionResult OnPost()
    {
        ModelState.Clear();

        var validationResult = _validator.Validate(Owner);
        if (validationResult.IsFailure)
        {
            ValidationErrors = validationResult.Errors;

            foreach (var error in validationResult.Errors)
            {
                var fieldName = MapErrorToField(error);
                if (!string.IsNullOrEmpty(fieldName))
                    ModelState.AddModelError($"Owner.{fieldName}", error);
                else
                    ModelState.AddModelError(string.Empty, error);
            }

            return Page();
        }

        var isSuccess = _ownerService.Update(Owner);

        if (!isSuccess)
        {
            ModelState.AddModelError(string.Empty, "No se pudo actualizar el registro.");
            return Page();
        }

        return RedirectToPage("/Owners/OwnerPage");
    }
    
    private string MapErrorToField(string error)
    {
        var errorLower = error.ToLower();

        if (errorLower.Contains("apellido paterno"))
            return "FirstLastname";

        if (errorLower.Contains("apellido materno"))
            return "SecondLastname";

        if (errorLower.Contains("nombre") && !errorLower.Contains("apellido"))
            return "Name";

        if (errorLower.Contains("teléfono"))
            return "PhoneNumber";

        if (errorLower.Contains("correo") || errorLower.Contains("email"))
            return "Email";

        if (errorLower.Contains("carnet") || errorLower.Contains(" ci ") ||
            errorLower.Contains("identidad"))
            return "Ci";

        if (errorLower.Contains("dirección"))
            return "Address";

        return string.Empty;
    }
}