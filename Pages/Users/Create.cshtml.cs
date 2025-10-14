// using FuerzaG.Application.Services;
// using FuerzaG.Domain.Entities;
// using FuerzaG.Infrastructure.Connection;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
//
// namespace FuerzaG.Pages.Users
// {
//     public class CreateModel : PageModel
//     {
//         private readonly UserService _userService;
//
//         [BindProperty]
//         public User User { get; set; } = new();
//
//         public CreateModel(IDbConnectionFactory connectionFactory)
//         {
//             _userService = new UserService(connectionFactory);
//         }
//
//         public void OnGet()
//         {
//         }
//
//         public IActionResult OnPost()
//         {
//             if (!ModelState.IsValid)
//                 return Page();
//
//             // Crear usuario mediante el servicio (UserService genera UserName y Password)
//             int newUserId = _userService.Create(User);
//
//             // Opcional: enviar correo con contraseña temporal si no se hace dentro del servicio
//             // En este ejemplo se asume que el servicio se encarga de ello o se implementará después
//
//             TempData["SuccessMessage"] = $"Usuario creado exitosamente.";
//             return RedirectToPage("/Users/UserPage");
//         }
//     }
// }
