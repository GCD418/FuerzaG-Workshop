using FuerzaG.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;


namespace FuerzaG.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly AccountService _accountService;
        private readonly IValidator<Account> _validator;

        public List<string> ValidationErrors { get; set; } = new List<string>();

        //public CreateModel(AccountService accountService, IValidator<Account> validator)
        //{
        //    _accountService = accountService;
        //    _validator = validator;
        //}

        //[BindProperty]
        //public Account Account { get; set; } = new();

        //// Para mostrar el username generado después de la creación
        //public string GeneratedUserName { get; set; } = string.Empty;

        //// Simulación de usuario actual; en producción usar claims o session
        //private Account CurrentUser => new Account { Role = "Propietario" };

        //public void OnGet() { }

        //public IActionResult OnPost()
        //{
        //    ValidationErrors.Clear();

        //    // Validar si el usuario actual puede asignar el rol seleccionado
        //    if (!CanCreate(CurrentUser, Account.Role))
        //    {
        //        ValidationErrors.Add("No tienes permiso para asignar este rol.");
        //        ModelState.AddModelError(string.Empty, "No tienes permiso para asignar este rol.");
        //        return Page();
        //    }

        //    // Validar los campos del Account
        //    var validationResult = _validator.Validate(Account);
        //    if (validationResult.IsFailure)
        //    {
        //        ValidationErrors = validationResult.Errors;
        //        foreach (var error in validationResult.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error);
        //        }
        //        return Page();
        //    }

        //    // Limpiar y sanear campos
        //    SanitizeAccountFields(Account);

        //    // Crear la cuenta (username y password generados automáticamente)
        //    var newId = _accountService.Create(Account);
        //    if (newId <= 0)
        //    {
        //        ModelState.AddModelError(string.Empty, "No se pudo crear el registro.");
        //        return Page();
        //    }

        //    // Mostrar el username generado
        //    GeneratedUserName = Account.UserName;

        //    // Limpiar modelo para crear otra cuenta si se desea
        //    Account = new Account();

        //    return Page();
        //}

        //private void SanitizeAccountFields(Account account)
        //{
        //    account.Name = account.Name.Trim();
        //    account.FirstLastName = account.FirstLastName.Trim();
        //    account.SecondLastName = account.SecondLastName?.Trim();
        //    account.Email = account.Email?.Trim();
        //    account.DocumentNumber = account.DocumentNumber.Trim();
        //    account.Role = account.Role.Trim();
        //}

        //private bool CanCreate(Account currentUser, string roleToAssign)
        //{
        //    switch (currentUser.Role.Trim().ToLower())
        //    {
        //        case "propietario":
        //            return roleToAssign.ToLower() switch
        //            {
        //                "administrador" => true,
        //                "technician" => true,
        //                "service" => true,
        //                "owner" => true,
        //                "vehicle" => true,
        //                _ => false
        //            };
        //        case "administrador":
        //            return roleToAssign.ToLower() switch
        //            {
        //                "owner" => true,
        //                "vehicle" => true,
        //                _ => false
        //            };
        //        default:
        //            return false;
        //    }
        //}
    }
}
