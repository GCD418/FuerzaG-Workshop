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
    [BindProperty] public Owner UserAccount { get; set; } = new();

    public IActionResult OnGet()
    {
        if (!ValidateSession(out var role)) return new EmptyResult();
        if (role != "Boss") return RedirectToPage("/Owners/OwnerPage");
        
        return Page();
    }

    public IActionResult OnPost()
    {
        // if (!ModelState.IsValid) return Page();
        var validationResult = _validator.Validate(UserAccount);
        if (validationResult.IsFailure)
        {
            ValidationErrors = validationResult.Errors;

            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
            return Page();
        }

        var newId = _ownerService.Create(UserAccount);
        if (newId <= 0)
        {
            ModelState.AddModelError(string.Empty, "No se pudo crear el registro.");
            return Page();
        }
        return RedirectToPage("/Owners/OwnerPage");
    }
}
