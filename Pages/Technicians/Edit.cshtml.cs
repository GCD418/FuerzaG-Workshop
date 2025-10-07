using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Factories;
using FuerzaG.Factories.ConcreteCreators;
using FuerzaG.Models;

namespace FuerzaG.Pages.Technicians
{
    public class EditModel : PageModel
    {
        private readonly TechnicianRepositoryCreator _creator;

        public EditModel(IDbConnectionFactory connectionFactory)
            => _creator = new TechnicianRepositoryCreator(connectionFactory);

        [BindProperty] public Technician Form { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var repo = _creator.GetRepository<Technician>();
            var entity = repo.GetById(id);
            if (entity is null) return RedirectToPage("/Technicians/TechnicianPage");


            Form = entity;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var ok = _creator.GetRepository<Technician>().Update(Form);
            if (!ok)
            {
                ModelState.AddModelError(string.Empty, "No se pudo actualizar.");
                return Page();
            }
            return RedirectToPage("/Technicians/TechnicianPage");
        }
    }
}
