using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.UsersAccounts
{
    
    [Authorize(Roles = UserRoles.CEO)]
    public class EditModel : PageModel
    {
        private readonly UserAccountService _accountService;
        private readonly IValidator<UserAccount> _validator;
        private readonly IDataProtector _protector;

        public EditModel(UserAccountService accountService, IValidator<UserAccount> validator, IDataProtectionProvider provider)
        {
            _accountService = accountService;
            _validator = validator;
            _protector = provider.CreateProtector("AccountProtector");
        }

        [BindProperty]
        public string EncryptedId { get; set; } = string.Empty;
        
        [BindProperty]
        public UserAccount UserAccount { get; set; } = new();

        private UserAccount CurrentUser => new UserAccount { Id = 1, Role = "Propietario" };

        public List<string> ValidationErrors { get; set; } = new List<string>();

        public IActionResult OnGet(string id)
        {
            var decryptedId = int.Parse(_protector.Unprotect(id));
            UserAccount = _accountService.GetById(decryptedId);

            if (UserAccount == null)
            {
                return Page(); 
            }

            EncryptedId = id;

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
