using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            
            if (!ModelState.IsValid)
            {
                return Page(); 
            }

            // Si la validación pasó, redirige a Index
            return RedirectToPage("/Index");
        }

        public class InputModel
        {
            [Display(Name = "Correo electrónico")]
            [Required(ErrorMessage = "El correo es obligatorio.")]
            [EmailAddress(ErrorMessage = "Ingresa un correo válido (debe contener @ y punto).")]
            public string Email { get; set; } = string.Empty;

            [Display(Name = "Contraseña")]
            [Required(ErrorMessage = "La contraseña es obligatoria.")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            [Display(Name = "Recordarme")]
            public bool RememberMe { get; set; }
        }
    }
}
