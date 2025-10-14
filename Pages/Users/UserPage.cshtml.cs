// using FuerzaG.Application.Services;
// using FuerzaG.Domain.Entities;
// using FuerzaG.Infrastructure.Connection;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
//
// namespace FuerzaG.Pages.Users;
//
// public class UserPageModel : PageModel
// {
//     public List<User> Users { get; set; } = new();
//     private readonly UserService _userService;
//
//     public UserPageModel(IDbConnectionFactory connectionFactory)
//     {
//         _userService = new UserService(connectionFactory);
//     }
//
//     // Carga la lista de usuarios al entrar a la página
//     public void OnGet()
//     {
//         Users = _userService.GetAll();
//     }
//
//     // Handler para eliminar un usuario desde el modal
//     public IActionResult OnPostDelete(int id)
//     {
//         _userService.DeleteById(id);
//         return RedirectToPage();
//     }
// }
