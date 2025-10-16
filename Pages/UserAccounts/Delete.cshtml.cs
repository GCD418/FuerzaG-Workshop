using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.UserAccounts
{
    public class DeleteModel : PageModel
    {
        private readonly UserAccountService _accountService;
        private readonly IValidator<UserAccount> _validator;

        public DeleteModel(UserAccountService accountService, IValidator<UserAccount> validator)
        {
            _accountService = accountService;
            _validator = validator;
        }

        [BindProperty]
        public UserAccount UserAccount { get; set; } = new();

        // Simulación de usuario actual (propietario, id=1)
        private UserAccount CurrentUser => _accountService.GetById(1);

        public IActionResult OnGet(int id)
        {
            UserAccount = _accountService.GetById(id);
            if (UserAccount == null)
                return NotFound();

            if (!(_validator is AccountValidator accountValidator) || !accountValidator.HasPermission(CurrentUser, "Propietario"))
            {
                ModelState.AddModelError(string.Empty, "No tienes permiso para eliminar cuentas.");
                return Page();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!(_validator is AccountValidator accountValidator) || !accountValidator.HasPermission(CurrentUser, "Propietario"))
            {
                ModelState.AddModelError(string.Empty, "No tienes permiso para eliminar cuentas.");
                return Page();
            }

            var success = _accountService.DeleteById(UserAccount.Id);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "No se pudo eliminar la cuenta.");
                return Page();
            }

            return RedirectToPage("/UserAccounts/AccountPage");
        }
    }
}
