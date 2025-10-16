using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly UserAccountService _accountService;
        private readonly IValidator<UserAccount> _validator;

        public List<string> ValidationErrors { get; set; } = new List<string>();

        [BindProperty]
        public UserAccount UserAccount { get; set; } = new();

        // Para mostrar el username generado después de la creación
        public string GeneratedUserName { get; set; } = string.Empty;

        public CreateModel(UserAccountService accountService, IValidator<UserAccount> validator)
        {
            _accountService = accountService;
            _validator = validator;
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            ValidationErrors.Clear();

            // Obtener el usuario actual desde su Id (simulación aquí; reemplazar con Claims o Session)
            var currentUserId = 1; // TODO: reemplazar por Id real del usuario autenticado
            var currentUser = _accountService.GetById(currentUserId);

            if (currentUser == null)
            {
                ModelState.AddModelError(string.Empty, "Usuario actual no encontrado.");
                return Page();
            }

            // Validar si el usuario actual puede asignar el rol seleccionado
            if (!CanCreate(currentUser, UserAccount.Role))
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
                    ModelState.AddModelError(string.Empty, error);
                return Page();
            }

            // Limpiar y sanear campos
            SanitizeUserAccountFields(UserAccount);

            // Crear la cuenta (username y password generados automáticamente)
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
