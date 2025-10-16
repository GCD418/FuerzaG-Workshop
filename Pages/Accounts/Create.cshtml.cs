using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly AccountService _accountService;
        private readonly IValidator<UserAccount> _validator;

        public List<string> ValidationErrors { get; set; } = new List<string>();

        public CreateModel(AccountService accountService, IValidator<UserAccount> validator)
        {
            _accountService = accountService;
            _validator = validator;
        }

        [BindProperty]
        public UserAccount UserAccount { get; set; } = new();

        // Para mostrar el username generado despu�s de la creaci�n
        public string GeneratedUserName { get; set; } = string.Empty;

        // Simulaci�n de usuario actual; en producci�n usar claims o session
        private UserAccount CurrentUser => new UserAccount { Role = "Propietario" };

        public void OnGet() { }

        public IActionResult OnPost()
        {
            ValidationErrors.Clear();

            // Validar si el usuario actual puede asignar el rol seleccionado
            if (!CanCreate(CurrentUser, UserAccount.Role))
            {
                ValidationErrors.Add("No tienes permiso para asignar este rol.");
                ModelState.AddModelError(string.Empty, "No tienes permiso para asignar este rol.");
                return Page();
            }

            // Validar los campos del UserAccount
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

            // Limpiar y sanear campos
            SanitizeUserAccountFields(UserAccount);

            // Crear la cuenta (username y password generados autom�ticamente)
            var newId = _accountService.Create(UserAccount);
            if (newId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se pudo crear el registro.");
                return Page();
            }

            // Mostrar el username generado
            GeneratedUserName = UserAccount.UserName;

            // Limpiar modelo para crear otra cuenta si se desea
            UserAccount = new UserAccount();

            return Page();
        }

        private void SanitizeUserAccountFields(UserAccount account)
        {
            account.Name = account.Name.Trim();
            account.FirstLastName = account.FirstLastName.Trim();
            account.SecondLastName = account.SecondLastName?.Trim();
            account.Email = account.Email?.Trim();
            account.DocumentNumber = account.DocumentNumber.Trim();
            account.Role = account.Role.Trim();
        }

        private bool CanCreate(UserAccount currentUser, string roleToAssign)
        {
            switch (currentUser.Role.Trim().ToLower())
            {
                case "propietario":
                    return roleToAssign.ToLower() switch
                    {
                        "administrador" => true,
                        "technician" => true,
                        "service" => true,
                        "owner" => true,
                        "vehicle" => true,
                        _ => false
                    };
                case "administrador":
                    return roleToAssign.ToLower() switch
                    {
                        "owner" => true,
                        "vehicle" => true,
                        _ => false
                    };
                default:
                    return false;
            }
        }
    }
}
