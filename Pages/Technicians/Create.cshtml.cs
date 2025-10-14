using FuerzaG.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;
using FuerzaG.Models;

namespace FuerzaG.Pages.Technicians
{
    public class CreateModel : PageModel
    {
        private readonly TechnicianRepositoryCreator _creator;
        private readonly TechnicianService _technicianService;

        public CreateModel(TechnicianService technicianService)
        {
            _technicianService = technicianService;
        }
        [BindProperty] public Technician Form { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var id = _technicianService.Create(Form);
            if (id <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se pudo crear el registro.");
                return Page();
            }
            return RedirectToPage("/Technicians/TechnicianPage");
        }
    }
}
