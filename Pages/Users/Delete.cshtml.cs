using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Models;
using FuerzaG.Application.Services;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Domain.Entities;

namespace FuerzaG.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly UserService _userService;

        [BindProperty]
        public new FuerzaG.Models.User User { get; set; } = new(); 

        public DeleteModel(IDbConnectionFactory connectionFactory)
        {
            _userService = new UserService(connectionFactory);
        }

        // Carga los datos del usuario para mostrar en el modal
        public IActionResult OnGet(int id)
        {
            var userEntity = _userService.GetById(id);
            if (userEntity == null)
                return RedirectToPage("/Users/UserPage"); // Usuario no encontrado

            // Mapeo a modelo de la página usando mapper privado
            User = MapToModel(userEntity);
            return Page();
        }

        // Se ejecuta al hacer POST desde el modal
        public IActionResult OnPost()
        {
            if (User == null || User.Id <= 0)
                return RedirectToPage("/Users/UserPage");

            bool deleted = _userService.DeleteById(User.Id);

            if (deleted)
                TempData["SuccessMessage"] = $"Usuario '{User.UserName}' eliminado exitosamente.";
            else
                TempData["SuccessMessage"] = $"No se pudo eliminar el usuario '{User.UserName}'.";

            return RedirectToPage("/Users/UserPage");
        }

        // --- Mapper privado para convertir entidad a modelo ---
        private FuerzaG.Models.User MapToModel(Domain.Entities.User entity) => new FuerzaG.Models.User
        {
            Id = entity.Id,
            Name = entity.Name,
            FirstLastName = entity.FirstLastName,
            SecondLastName = entity.SecondLastName,
            Ci = entity.Ci,
            UserName = entity.UserName,
            Role = entity.Role,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            IsActive = entity.IsActive,
            ModifiedByUserId = entity.ModifiedByUserId
        };
    }
}
