using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Domain.Entities;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;

namespace FuerzaG.Pages.Owners;

public class DeleteModel : PageModel
{
    private readonly OwnerRepositoryCreator _creator;

    public DeleteModel(IDbConnectionFactory connectionFactory)
        => _creator = new OwnerRepositoryCreator(connectionFactory);


    public IActionResult OnPost(int id)
    {
        _creator.GetRepository<Owner>().DeleteById(id);
        return RedirectToPage("/Owners/OwnerPage");
    }
}
