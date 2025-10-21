using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.UserAccounts
{
    
    [Authorize(Roles = UserRoles.CEO)]
    public class AccountPageModel : PageModel
    {
        private readonly UserAccountService _accountService;
        private readonly IValidator<UserAccount> _validator;
        private readonly IDataProtector _protector;

        public List<UserAccount> Accounts { get; set; } = new();

        [BindProperty]
        public UserAccount UserAccount { get; set; } = new();

        public AccountPageModel(UserAccountService accountService, IValidator<UserAccount> validator, IDataProtectionProvider provider)
        {
            _accountService = accountService;
            _validator = validator;
            _protector = provider.CreateProtector("AccountProtector");
        }

        public void OnGet()
        {
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            Accounts = _accountService.GetAll();
        }
        
        public string EncryptId(int id)
        {
            return _protector.Protect(id.ToString());
        }

        public IActionResult OnPostDelete(string id)
        {
            var decryptedId = int.Parse(_protector.Unprotect(id));
            var currentUser = _accountService.GetById(1);
            if (currentUser == null || (!(_validator is AccountValidator accountValidator) || !accountValidator.HasPermission(UserAccount, "Propietario")))
            {
                ModelState.AddModelError(string.Empty, "No tienes permiso para eliminar cuentas.");
                LoadAccounts();
                return Page();
            }

            var success = _accountService.DeleteById(decryptedId);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "No se pudo eliminar la cuenta.");
            }

            LoadAccounts();
            return Page();
        }
    }
}
