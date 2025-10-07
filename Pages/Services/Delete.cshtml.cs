using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Factories;
using FuerzaG.Factories.ConcreteCreators;
using FuerzaG.Data.Interfaces;
using FuerzaG.Models;

namespace FuerzaG.Pages.Services;

public class DeleteModel : PageModel
{
    private readonly ServiceRepositoryCreator _creator;

    public DeleteModel(IDbConnectionFactory connectionFactory)
        => _creator = new ServiceRepositoryCreator(connectionFactory);

    // POST /Services/Delete?id=123
    public IActionResult OnPost(int id)
    {
        _creator.GetRepository<Service>().DeleteById(id);
        return RedirectToPage("/ServicePage");
    }
}
