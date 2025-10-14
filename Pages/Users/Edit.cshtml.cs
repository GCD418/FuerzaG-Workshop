using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Models;
using FuerzaG.Application.Services;
using FuerzaG.Infrastructure.Connection;
using DomainUser = FuerzaG.Domain.Entities.User; // Alias para evitar ambigüedad

namespace FuerzaG.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly UserService _userService;

        [BindProperty]
        public new User User { get; set; } = new(); // 'new' para ocultar PageModel.User

        public EditModel(IDbConnectionFactory connectionFactory)
        {
            _userService = new UserService(connectionFactory);
        }

        // Carga los datos del usuario para editar
        public IActionResult OnGet(int id)
        {
            var userEntity = _userService.GetById(id);
            if (userEntity == null)
                return RedirectToPage("/Users/UserPage");

            User = MapToModel(userEntity);
            return Page();
        }

        // Guardar los cambios
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            bool updated = _userService.Update(MapToEntity(User));

            if (updated)
                TempData["SuccessMessage"] = $"Usuario '{User.UserName}' actualizado exitosamente.";
            else
                TempData["SuccessMessage"] = $"No se pudo actualizar el usuario '{User.UserName}'.";

            return RedirectToPage("/Users/UserPage");
        }

        // --- Mappers privados ---
        private User MapToModel(DomainUser entity) => new User
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

        private DomainUser MapToEntity(User model) => new DomainUser
        {
            Id = model.Id,
            Name = model.Name,
            FirstLastName = model.FirstLastName,
            SecondLastName = model.SecondLastName,
            Ci = model.Ci,
            UserName = model.UserName,
            Role = model.Role,
            IsActive = model.IsActive,
            ModifiedByUserId = model.ModifiedByUserId
        };
    }
}
