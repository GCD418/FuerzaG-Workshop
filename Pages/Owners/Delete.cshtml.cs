using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Owners;


[Authorize(Roles = UserRoles.Manager)]
public class DeleteModel : PageModel
{
    private readonly OwnerService  _ownerService;
    private readonly IDataProtector _protector;

    public DeleteModel(OwnerService ownerService, IDataProtectionProvider provider)
    {
        _ownerService = ownerService;
        _protector = provider.CreateProtector("OwnerProtector");
    }

    public void OnGet()
    { }

    public IActionResult OnPost(string id)
    {
        var decryptedId = int.Parse(_protector.Unprotect(id));
        _ownerService.DeleteById(decryptedId);
        return RedirectToPage("/Owners/OwnerPage");
    }
}
