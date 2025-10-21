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

        public async Task<IActionResult> OnPost()
        {
            
            if (!ModelState.IsValid)
            {
                return Page(); 
            }

            bool isSuccess = await _loginService.LogIn(Input.Username, Input.Password);
            // Si la validación pasó, redirige a Index
            if (!isSuccess)
            {
                var ErrorMessage = "Usuario o contraseña incorrectos.";
                ModelState.AddModelError(string.Empty, ErrorMessage);
                return Page();
            }
            return RedirectToPage("/Index");
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
