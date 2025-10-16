using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.UsersAccounts
{
    public class EditModel : PageModel
    {
        private readonly UserAccountService _accountService;
        private readonly IValidator<UserAccount> _validator;

        public EditModel(UserAccountService accountService, IValidator<UserAccount> validator)
        {
            _accountService = accountService;
            _validator = validator;
        }

        [BindProperty]
        public UserAccount UserAccount { get; set; } = new();

        private UserAccount CurrentUser => new UserAccount { Id = 1, Role = "Propietario" };

        public List<string> ValidationErrors { get; set; } = new List<string>();

        public IActionResult OnGet(int id)
        {
            UserAccount = _accountService.GetById(id);

            if (UserAccount == null)
            {
                return Page(); 
            }

            if (!(_validator is AccountValidator accountValidator) || !accountValidator.HasPermission(CurrentUser, "Propietario"))
            {
                ModelState.AddModelError(string.Empty, "No tienes permiso para editar cuentas.");
                return Page();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            ValidationErrors.Clear();

            if (!(_validator is AccountValidator accountValidator) || !accountValidator.HasPermission(CurrentUser, "Propietario"))
                {
                ValidationErrors.Add("No tienes permiso para editar cuentas.");
                ModelState.AddModelError(string.Empty, "No tienes permiso para editar cuentas.");
                return Page();
            }

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

            var success = _accountService.Update(UserAccount);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "No se pudo actualizar la cuenta.");
                return Page();
            }

            return RedirectToPage("/UserAccounts/AccountPage");
        }
    }
}
