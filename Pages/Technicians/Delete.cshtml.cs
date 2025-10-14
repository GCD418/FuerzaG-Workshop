using FuerzaG.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;

namespace FuerzaG.Pages.Technicians
{
    public class DeleteModel : PageModel
    {
        private readonly TechnicianRepositoryCreator _creator;
        private readonly TechnicianService  _technicianService;

        public DeleteModel(TechnicianService technicianService)
        {
            _technicianService = technicianService;
        }
        public IActionResult OnPost(int id)
        {
            _technicianService.DeleteById(id);
            return RedirectToPage("/Technicians/TechnicianPage");

        }
    }
}
