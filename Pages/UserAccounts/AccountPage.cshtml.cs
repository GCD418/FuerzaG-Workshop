using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.UserAccounts
{
    public class AccountPageModel : PageModel
    {
        private readonly UserAccountService _accountService;
        private readonly IValidator<UserAccount> _validator;

        public List<UserAccount> Accounts { get; set; } = new();

        [BindProperty]
        public UserAccount UserAccount { get; set; } = new();

        public AccountPageModel(UserAccountService accountService, IValidator<UserAccount> validator)
        {
            _accountService = accountService;
            _validator = validator;
        }

        public void OnGet()
        {
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            Accounts = _accountService.GetAll();
        }

        public IActionResult OnPostDelete(int id)
        {
            // Obtener el usuario actual
            var currentUser = _accountService.GetById(1); // Hardcodeado: propietario, id=1
            if (currentUser == null || (!(_validator is AccountValidator accountValidator) || !accountValidator.HasPermission(UserAccount, "Propietario")))
            {
                ModelState.AddModelError(string.Empty, "No tienes permiso para eliminar cuentas.");
                LoadAccounts();
                return Page();
            }

            var success = _accountService.DeleteById(id);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "No se pudo eliminar la cuenta.");
            }

            LoadAccounts();
            return Page();
        }
    }
}
