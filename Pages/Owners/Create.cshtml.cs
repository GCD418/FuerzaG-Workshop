using FuerzaG.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;
using FuerzaG.Pages.Shared;

namespace FuerzaG.Pages.Owners;

public class CreateModel : SecurePageModel
{
    private readonly OwnerService  _ownerService;
    private readonly IValidator<Owner> _validator;
    public List<string> ValidationErrors { get; set; } = [];

    public CreateModel(OwnerService ownerService, IValidator<Owner> validator)
    {
        _ownerService = ownerService;
        _validator = validator;
    }
    [BindProperty] public Owner Owner { get; set; } = new();

    public IActionResult OnGet()
    {
        if (!ValidateSession(out var role)) return new EmptyResult();
        if (role != UserRoles.Manager) return RedirectToPage("/Owners/OwnerPage");
        
        return Page();
    }

    public IActionResult OnPost()
    {
        ModelState.Clear();
        // if (!ModelState.IsValid) return Page();
        var validationResult = _validator.Validate(Owner);
        if (validationResult.IsFailure)
        {
            ValidationErrors = validationResult.Errors;

            foreach (var error in validationResult.Errors)
            { 
                var fieldName = MapErrorToField(error);
                    
                if (!string.IsNullOrEmpty(fieldName))
                {
                    ModelState.AddModelError($"Owner.{fieldName}", error);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            return Page();
        }

        var newId = _ownerService.Create(Owner);
        if (newId <= 0)
        {
            ModelState.AddModelError(string.Empty, "No se pudo crear el registro.");
            return Page();
        }
        return RedirectToPage("/Owners/OwnerPage");
    }
    private string MapErrorToField(string error)
    {
        var errorLower = error.ToLower();
        
        // Orden importante: primero los más específicos
        if (errorLower.Contains("apellido paterno"))
            return "FirstLastname";
        
        if (errorLower.Contains("apellido materno"))
            return "SecondLastname";
        
        // Después el nombre (para evitar conflicto con "apellido")
        if (errorLower.Contains("nombre") && !errorLower.Contains("apellido"))
            return "Name";
        
        if (errorLower.Contains("teléfono"))
            return "PhoneNumber";
        
        if (errorLower.Contains("correo") || errorLower.Contains("email"))
            return "Email";
        
        // Carnet de identidad tiene varias formas
        if (errorLower.Contains("carnet") || errorLower.Contains(" ci ") || 
            errorLower.Contains("identidad"))
            return "Ci";
        
        if (errorLower.Contains("dirección"))
            return "Address";
        
        return string.Empty;
    }
}
