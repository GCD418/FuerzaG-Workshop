using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Domain.Services.Validations;

namespace FuerzaG.Pages.Owners;

public class EditModel : PageModel
{
    private readonly OwnerService  _ownerService;
    private readonly IDataProtector _protector;

    public EditModel(OwnerService ownerService, IDataProtectionProvider provider)
    {
        _ownerService = ownerService;
        _protector = provider.CreateProtector("OwnerProtector");
    }

    
    [BindProperty] public string EncryptedId { get; set; } = string.Empty;
    [BindProperty] public int Id { get; set; }
    [BindProperty] public string Name { get; set; } = string.Empty;
    [BindProperty] public string FirstLastname { get; set; } = string.Empty;
    [BindProperty] public string? SecondLastname { get; set; }
    [BindProperty] public int PhoneNumber { get; set; }
    [BindProperty] public string Email { get; set; } = string.Empty;
    [BindProperty] public string Ci { get; set; } = string.Empty;
    [BindProperty] public string Address { get; set; } = string.Empty;

    
    public IActionResult OnGet(string id)
    {
        var decryptedId = int.Parse(_protector.Unprotect(id));
        Owner owner = _ownerService.GetById(decryptedId);
        if (owner is null) return RedirectToPage("/Owners/OwnerPage");

        EncryptedId = id;
        Id = owner.Id;
        Name = owner.Name;
        FirstLastname = owner.FirstLastname;
        SecondLastname = owner.SecondLastname;
        PhoneNumber = owner.PhoneNumber;
        Email = owner.Email;
        Ci = owner.Ci;
        Address = owner.Address;

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
