using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;
using FuerzaG.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Sessions;

[Authorize(Roles = UserRoles.CEO)]
public class CreateModel : PageModel
{
    private readonly LoginService  _loginService;
    private readonly IValidator<Owner> _validator;
    public List<string> ValidationErrors { get; set; } = [];

    public CreateModel(LoginService loginService, IValidator<Owner> validator)
    {
        _loginService = loginService;
        _validator = validator;
    }
    [BindProperty] public UserAccount UserAccount { get; set; } = new();

    public void OnGet()
    { }

    public IActionResult OnPost()
    {
        // if (!ModelState.IsValid) return Page();
        // var validationResult = _validator.Validate(UserAccount);
        // if (validationResult.IsFailure)
        // {
        //     ValidationErrors = validationResult.Errors;
        //
        //     foreach (var error in validationResult.Errors)
        //     {
        //         ModelState.AddModelError(string.Empty, error);
        //     }
        //     return Page();
        // }

        var isSuccess = _loginService.CreateUserAccount(UserAccount);
        if (!isSuccess)
        {
            ModelState.AddModelError(string.Empty, "No se pudo crear el usuario.");
            return Page();
        }
        return RedirectToPage("/UserAccounts/AccountPage");
    }
}
