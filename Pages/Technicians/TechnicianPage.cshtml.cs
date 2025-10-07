using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Factories;
using FuerzaG.Factories.ConcreteCreators;
using FuerzaG.Models;

namespace FuerzaG.Pages.Technicians
{
    public class TechnicianPageModel : PageModel
    {
        private readonly TechnicianRepositoryCreator _creator;

        public TechnicianPageModel(IDbConnectionFactory connectionFactory)
            => _creator = new TechnicianRepositoryCreator(connectionFactory);

        public List<Technician> List { get; set; } = new();

        public void OnGet()
        {
            var repo = _creator.GetRepository<Technician>();
            List = new List<Technician>(repo.GetAll());
        }

        public IActionResult OnPostDelete(int id)
        {
            _creator.GetRepository<Technician>().DeleteById(id);
            return RedirectToPage("/Technicians/TechnicianPage");
        }
    }
}
