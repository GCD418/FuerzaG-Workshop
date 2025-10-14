using FuerzaG.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Models;

namespace FuerzaG.Pages.Technicians
{
    public class EditModel : PageModel
    {
        private readonly TechnicianService _technicianService;

        public EditModel(TechnicianService technicianService)
        {
            _technicianService = technicianService;
        }
        [BindProperty] public Technician Form { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var entity = _technicianService.GetById(id);
            if (entity is null) return RedirectToPage("/Technicians/TechnicianPage");

            Form = entity;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var ok = _technicianService.Update(Form);
            if (!ok)
            {
                ModelState.AddModelError(string.Empty, "No se pudo actualizar.");
                return Page();
            }
            return RedirectToPage("/Technicians/TechnicianPage");
        }
    }
}
