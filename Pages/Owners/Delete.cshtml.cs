using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Factories;                       // IDbConnectionFactory
using FuerzaG.Factories.ConcreteCreators;      // OwnerRepositoryCreator
using FuerzaG.Data.Interfaces;                 // IRepository<T>
using FuerzaG.Models;                          // (opcional)

namespace FuerzaG.Pages.Owners;

public class DeleteModel : PageModel
{
    private readonly OwnerRepositoryCreator _creator;

    public DeleteModel(IDbConnectionFactory connectionFactory)
        => _creator = new OwnerRepositoryCreator(connectionFactory);

    // POST /Owners/Delete?id=123
    public IActionResult OnPost(int id)
    {
        _creator.GetRepository<Owner>().DeleteById(id);
        return RedirectToPage("/OwnerPage");
    }
}
