using FuerzaG.Application.Services;
using Microsoft.AspNetCore.DataProtection;
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
        private readonly IDataProtector _protector;

        public DeleteModel(TechnicianService technicianService, IDataProtectionProvider provider)
        {
            _technicianService = technicianService;
            _protector = provider.CreateProtector("TechnicianProtector");
        }
        
        public IActionResult OnPost(string id)
        {
            var decryptedId = int.Parse(_protector.Unprotect(id));
            _technicianService.DeleteById(decryptedId);
            return RedirectToPage("/Technicians/TechnicianPage");

        }
    }
}
