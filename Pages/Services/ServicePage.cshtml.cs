using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Models;                         // Service (modelo)
using FuerzaG.Factories.ConcreteCreators;     // ServiceRepositoryCreator
using FuerzaG.Infrastructure.Connection;      // IDbConnectionFactory

namespace FuerzaG.Pages.Services
{
    public class ServicePage : PageModel
    {
        public List<Service> Services { get; set; } = new();

        private readonly ServiceRepositoryCreator _factory;

        public ServicePage(IDbConnectionFactory connectionFactory)
            => _factory = new ServiceRepositoryCreator(connectionFactory);

        public void OnGet()
            => Services = _factory.GetRepository<Service>().GetAll();

        public IActionResult OnPostDelete(int id)
        {
            _factory.GetRepository<Service>().DeleteById(id);
            return RedirectToPage(); 
        }
    }
}