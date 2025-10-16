using System.ComponentModel.DataAnnotations;
using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages
{
    public class LoginModel : PageModel
    {
        private readonly LoginService _loginService;
        [BindProperty]
        public InputModel Input { get; set; } = new();

        public LoginModel(LoginService loginService)
        {
            _loginService = loginService;
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            
            if (!ModelState.IsValid)
            {
                return Page(); 
            }

            UserAccount userAccount = _loginService.LogIn(Input.Username, Input.Password);
            // Si la validación pasó, redirige a Index
            if (userAccount == null)
            {
                var ErrorMessage = "Usuario o contraseña incorrectos.";
                ModelState.AddModelError(string.Empty, ErrorMessage);
                return Page();
            }

            HttpContext.Session.SetString("userName", userAccount.UserName);
            HttpContext.Session.SetString("role", userAccount.Role);
            return RedirectToPage("/Owners/OwnerPage");
        }

        public class InputModel
        {
            [Display(Name = "Nombre de usuario")]
            [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
            public string Username { get; set; } = string.Empty;

            [Display(Name = "Contraseña")]
            [Required(ErrorMessage = "La contraseña es obligatoria.")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            [Display(Name = "Recordarme")]
            public bool RememberMe { get; set; }
        }
    }
}
