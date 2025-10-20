using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Models;
using FuerzaG.Pages.Shared;

namespace FuerzaG.Pages.Technicians
{
    public class EditModel : SecurePageModel
    {
        private readonly TechnicianService _technicianService;
        private readonly IDataProtector _protector;

        public EditModel(TechnicianService technicianService, IDataProtectionProvider provider)
        {
            _technicianService = technicianService;
            _protector = provider.CreateProtector("TechnicianProtector");
        }
        
        [BindProperty] public string EncryptedId { get; set; } = string.Empty;
        [BindProperty] public Technician Form { get; set; } = new();

        public IActionResult OnGet(string id)
        {
            if (!ValidateSession(out var role)) return new EmptyResult();
            if (role != UserRoles.Manager) return RedirectToPage("/Technicians/TechnicianPage");
            var decryptedId = int.Parse(_protector.Unprotect(id));
            var entity = _technicianService.GetById(decryptedId);
            if (entity is null) return RedirectToPage("/Technicians/TechnicianPage");

            EncryptedId = id;
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
