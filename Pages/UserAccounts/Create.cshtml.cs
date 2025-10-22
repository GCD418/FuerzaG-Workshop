using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace FuerzaG.Pages.UserAccounts
{
    
    [Authorize(Roles = UserRoles.CEO)]
    public class CreateModel : PageModel
    {
        private readonly UserAccountService _accountService;
        private readonly IValidator<UserAccount> _validator;

        [BindProperty]
        public UserAccountModel UserAccount { get; set; } = new UserAccountModel();

        public List<string> ValidationErrors { get; set; } = new List<string>();

        public string GeneratedUserName { get; set; }

        public CreateModel(UserAccountService accountService, IValidator<UserAccount> validator)
        {
            _accountService = accountService;
            _validator = validator;
        }

        public void OnGet(int currentUserId)
        {
            // Inicializaci�n si es necesario
        }

        public IActionResult OnPost(int currentUserId)
        {
            ValidationErrors.Clear();

            // Validaci�n simple de ejemplo
            if (string.IsNullOrWhiteSpace(UserAccount.Name))
                ValidationErrors.Add("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(UserAccount.Email))
                ValidationErrors.Add("El correo electrónico es obligatorio.");

            if (ValidationErrors.Count > 0)
                return Page();

            // Simulaci�n de creaci�n de usuario
            GeneratedUserName = $"{UserAccount.Name}.{UserAccount.FirstLastName}".ToLower();

            // Aqu� ir�a la l�gica para guardar el usuario y enviar el correo

            return Page();
        }
    }

    public class UserAccountModel
    {
        [Required]
        public string Name { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string DocumentNumber { get; set; }
        public string Role { get; set; }
    }
}
