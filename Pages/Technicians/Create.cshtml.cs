using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Models;
using FuerzaG.Domain.Services.Validations;
using FuerzaG.Application.Services;

namespace FuerzaG.Pages.Technicians
{
    public class CreateModel : PageModel
    {
        private readonly IValidator<Technician> _validator;
        private readonly TechnicianService _technicianService;

        public List<string> ValidationErrors { get; set; } = new();

        [BindProperty]
        public Technician Form { get; set; } = new();

        public CreateModel(IValidator<Technician> validator, TechnicianService technicianService)
        {
            _validator = validator;
            _technicianService = technicianService;
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            // Validación con el patrón Result
            var validationResult = _validator.Validate(Form);

            if (validationResult.IsFailure)
            {
                ValidationErrors = validationResult.Errors;
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("Form.Name", error); // <— en vez de string.Empty

                return Page();
            }


            if (!ModelState.IsValid)
                return Page();

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
