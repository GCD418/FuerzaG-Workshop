using FuerzaG.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;
using FuerzaG.Models;
using FuerzaG.Domain.Services.Validations;

namespace FuerzaG.Pages.Technicians
{
    public class CreateModel : PageModel
    {
        private readonly IValidator<Technician> _validator;
        private readonly TechnicianRepositoryCreator _creator;
        private readonly TechnicianService _technicianService;
        public List<string> ValidationErrors { get; set; } = [];
        public CreateModel(TechnicianService technicianService, IValidator<Technician> validator)
        {
            _technicianService = technicianService;
            _validator = validator;
        }
        [BindProperty] public Technician Form { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            var validationResult = _validator.Validate(Form);
            if (validationResult.IsFailure)
            {
                ValidationErrors = validationResult.Errors;
                foreach(var error in validationResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return Page();
            }
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
