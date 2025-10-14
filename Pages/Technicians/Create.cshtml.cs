using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Factories;
using FuerzaG.Factories.ConcreteCreators;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Models;

namespace FuerzaG.Pages.Technicians
{
    public class CreateModel : PageModel
    {
        private readonly TechnicianRepositoryCreator _creator;

        public CreateModel(IDbConnectionFactory connectionFactory)
            => _creator = new TechnicianRepositoryCreator(connectionFactory);

        [BindProperty] public Technician Form { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var id = _creator.GetRepository<Technician>().Create(Form);
            if (id <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se pudo crear el registro.");
                return Page();
            }
            return RedirectToPage("/Technicians/TechnicianPage");
        }
    }
}
