using FuerzaG.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Owners;

public class DeleteModel : PageModel
{
    private readonly OwnerService  _ownerService;

    public DeleteModel(OwnerService ownerService)
    {
        _ownerService = ownerService;
    }


    public IActionResult OnPost(int id)
    {
        _ownerService.DeleteById(id);
        return RedirectToPage("/Owners/OwnerPage");
    }
}
