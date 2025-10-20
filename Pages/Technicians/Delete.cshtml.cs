using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;
using FuerzaG.Pages.Shared;

namespace FuerzaG.Pages.Technicians
{
    public class DeleteModel : SecurePageModel
    {
        private readonly TechnicianService  _technicianService;
        private readonly IDataProtector _protector;

        public DeleteModel(TechnicianService technicianService, IDataProtectionProvider provider)
        {
            _technicianService = technicianService;
            _protector = provider.CreateProtector("TechnicianProtector");
        }

        public IActionResult OnGet()
        {
            if (!ValidateSession(out var role)) return new EmptyResult();
            if (role != UserRoles.Manager) return RedirectToPage("/Technicians/TechnicianPage");
            return Page();
        }
        
        public IActionResult OnPost(string id)
        {
            var decryptedId = int.Parse(_protector.Unprotect(id));
            _technicianService.DeleteById(decryptedId);
            return RedirectToPage("/Technicians/TechnicianPage");

        }
    }
}
