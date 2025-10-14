using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;

namespace FuerzaG.Pages.Technicians
{
    public class DeleteModel : PageModel
    {
        private readonly TechnicianRepositoryCreator _creator;

        public DeleteModel(IDbConnectionFactory connectionFactory)
            => _creator = new TechnicianRepositoryCreator(connectionFactory);

        public IActionResult OnPost(int id)
        {
            _creator.GetRepository<Models.Technician>().DeleteById(id);
            return RedirectToPage("/Technicians/TechnicianPage");

        }
    }
}
