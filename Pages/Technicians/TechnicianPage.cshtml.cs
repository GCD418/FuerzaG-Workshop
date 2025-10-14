using System.Collections.Generic;
using FuerzaG.Application.Services;
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

        public TechnicianPageModel(TechnicianService technicianService)
        {
            _technicianService = technicianService;
        }

        public void OnGet()
        {
            Technicians = _technicianService.GetAll();
        }

        public IActionResult OnPostDelete(int id)
        {
            _technicianService.DeleteById(id);
            return RedirectToPage("/Technicians/TechnicianPage");
        }
    }
}
