using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

using FuerzaG.Factories;
using FuerzaG.Factories.ConcreteCreators;
using FuerzaG.Data.Interfaces;
using FuerzaG.Models;

namespace FuerzaG.Pages.Services
{
    public class ServicePageModel : PageModel
    {
        private readonly ServiceRepositoryCreator _creator;

        public ServicePageModel(IDbConnectionFactory connectionFactory)
        {
            _creator = new ServiceRepositoryCreator(connectionFactory);
        }

        public List<Service> Services { get; set; } = new();

        public void OnGet()
        {
            var repo = _creator.GetRepository<Service>();
            
            Services = repo.GetAll().Where(s => s.IsActive).ToList();
        }

        public IActionResult OnPostDelete(int id)
        {
            var repo = _creator.GetRepository<Service>();
            var service = repo.GetById(id);

            if (service == null)
                return RedirectToPage("/ServicePage");

            
            service.IsActive = false;
            repo.Update(service);

            return RedirectToPage("/ServicePage");
        }
    }
}
