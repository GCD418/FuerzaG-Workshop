using System.Collections.Generic;
using FuerzaG.Application.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;
using FuerzaG.Models;

namespace FuerzaG.Pages.Technicians
{
    public class TechnicianPageModel : PageModel
    {
        public List<Technician> Technicians { get; set; } = new();
        private readonly TechnicianService _technicianService;
        private readonly IDataProtector _protector;

        public TechnicianPageModel(TechnicianService technicianService, IDataProtectionProvider provider)
        {
            _technicianService = technicianService;
            _protector = provider.CreateProtector("TechnicianProtector");
        }

        public void OnGet()
        {
            Technicians = _technicianService.GetAll();
        }
        
        public string EncryptId(int id)
        {
            return _protector.Protect(id.ToString());
        }

        public IActionResult OnPostDelete(string id)
        {
            var decryptedId = int.Parse(_protector.Unprotect(id));
            _technicianService.DeleteById(decryptedId);
            return RedirectToPage("/Technicians/TechnicianPage");
        }
    }
}
